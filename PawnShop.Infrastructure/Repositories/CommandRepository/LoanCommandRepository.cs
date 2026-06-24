using Microsoft.EntityFrameworkCore;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Domain.Entities;
using PawnShop.Domain.Enums;
using PawnShop.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.CommandRepository
{
    public class LoanCommandRepository : ILoanCommandRepository
    {
        private readonly PawnShopDbContext _context;
        public LoanCommandRepository(PawnShopDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateLoanAsync(CreateLoanRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(x =>
                        x.CustomerId == request.CustomerId &&
                        x.TenantId == request.TenantId);

                if (customer == null)
                    throw new InvalidOperationException("Customer not found");

                var currentCapital =
                    await _context.CapitalTransactions
                        .OrderByDescending(x => x.TransactionDate)
                        .Select(x => x.BalanceAfterTransaction)
                        .FirstOrDefaultAsync();

                if (currentCapital < request.PrincipalAmount)
                    throw new InvalidOperationException("Insufficient capital");

                var loanNumber =
                    $"LN-{request.CustomerId}{DateTime.UtcNow:yyyyMMddHHmmss}";

                var loan = new Loan
                {
                    TenantId = request.TenantId,
                    CustomerId = request.CustomerId,
                    LoanNumber = loanNumber,
                    PrincipalAmount = request.PrincipalAmount,
                    InterestPercentage = request.InterestPercentage,
                    LoanDate = DateTime.UtcNow,
                    DueDate = request.MaturityDate,
                    Status = LoanStatus.Active,
                    CreatedOn = DateTime.UtcNow,
                };

                _context.Loans.Add(loan);

                await _context.SaveChangesAsync();

                foreach (var item in request.GoldItems)
                {
                    _context.GoldItems.Add(new GoldItem
                    {
                        TenantId = request.TenantId,
                        LoanId = loan.LoanId,
                        ItemType = (GoldItemType)item.ItemType,
                        Weight = item.Weight,
                        Purity = item.Purity,
                        Description = item.Description
                    });
                }

                _context.CapitalTransactions.Add(
                    new CapitalTransaction
                    {
                        TenantId = request.TenantId,
                        LoanId = loan.LoanId,
                        TransactionType =
                            CapitalTransactionType.LoanIssued,
                        Amount = request.PrincipalAmount,
                        BalanceAfterTransaction =
                            currentCapital -
                            request.PrincipalAmount,
                        TransactionDate = DateTime.UtcNow,
                        Remarks =
                            $"Loan issued {loanNumber}"
                    });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return loanNumber;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> CreatePaymentAsync(CreatePaymentRequest request)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var loan = await _context.Loans
                    .FirstOrDefaultAsync(x =>
                        x.LoanId == request.LoanId &&
                        x.TenantId == request.TenantId);

                var totalAmount =
                            request.PrincipalAmount +
                            request.InterestAmount +
                            request.PenaltyAmount +
                            request.ServiceFee;

                if (loan == null)
                    throw new InvalidOperationException("Loan not found");

                if (loan.Status != LoanStatus.Active)
                    throw new InvalidOperationException("Loan already closed");

                var payment = new Payment
                {
                    TenantId = request.TenantId,
                    LoanId = loan.LoanId,
                    PrincipalAmount = request.PrincipalAmount,
                    InterestAmount = request.InterestAmount,
                    PenaltyAmount = request.PenaltyAmount,
                    ServiceFee = request.ServiceFee,
                    TotalAmount = totalAmount,
                    PaymentType = request.TransactionType,
                    PaymentDate = DateTime.UtcNow,
                    Remarks = request.Remarks
                };

                _context.Payments.Add(payment);

                await _context.SaveChangesAsync();

                if (request.InterestAmount > 0)
                {
                    var currentProfit =
                        await _context.ProfitTransactions
                            .OrderByDescending(x =>
                                x.TransactionDate)
                            .Select(x =>
                                x.BalanceAfterTransaction)
                            .FirstOrDefaultAsync();

                    _context.ProfitTransactions.Add(
                        new ProfitTransaction
                        {
                            TenantId = request.TenantId,
                            PaymentId = payment.PaymentId,
                            TransactionType =
                                ProfitTransactionType
                                    .InterestReceived,
                            Amount = payment.InterestAmount,
                            BalanceAfterTransaction =
                                currentProfit +
                                payment.InterestAmount + payment.ServiceFee + payment.PenaltyAmount,
                            TransactionDate =
                                DateTime.UtcNow
                        });
                }
                if (request.PrincipalAmount > 0)
                {
                    var currentCapital =
                        await _context.CapitalTransactions
                            .OrderByDescending(x =>
                                x.TransactionDate)
                            .Select(x =>
                                x.BalanceAfterTransaction)
                            .FirstOrDefaultAsync();

                    _context.CapitalTransactions.Add(
                        new CapitalTransaction
                        {
                            TenantId = request.TenantId,
                            LoanId = loan.LoanId,
                            PaymentId = payment.PaymentId,
                            TransactionType =
                                CapitalTransactionType
                                    .PrincipalReceived,
                            Amount = payment.PrincipalAmount,
                            BalanceAfterTransaction =
                                currentCapital +
                                payment.PrincipalAmount,
                            TransactionDate =
                                DateTime.UtcNow
                        });
                }
                if(request.CloseLoan)
                {
                    var totalPrincipalPaid =
                                        await _context.Payments
                                            .Where(x => x.LoanId == loan.LoanId)
                                            .SumAsync(x => x.PrincipalAmount);

                    var principalAfterPayment =
                        totalPrincipalPaid +
                        request.PrincipalAmount;

                    if (request.CloseLoan &&
                       principalAfterPayment < loan.PrincipalAmount)
                    {
                        throw new InvalidOperationException(
                            "Loan cannot be closed. Principal still pending.");
                    }

                    loan.Status = LoanStatus.Closed;
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
