using Microsoft.EntityFrameworkCore;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class LoanQueryRepository : ILoanQueryRepository
{
    private readonly PawnShopDbContext _context;

    public LoanQueryRepository(PawnShopDbContext context) => _context = context;

    public async Task<IEnumerable<LoanDto>> GetLoansByTenantAsync(Guid tenantId)
    {
        return await _context.Loans
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId)
            .Include(x => x.Customer)
            .Select(x => new LoanDto
            {
                LoanId = x.LoanId,
                LoanNumber = x.LoanNumber,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer != null ? x.Customer.Name : string.Empty,
                PrincipalAmount = x.PrincipalAmount,
                InterestPercentage = x.InterestPercentage,
                LoanDate = x.LoanDate,
                DueDate = x.DueDate,
                Status = x.Status
            })
            .ToListAsync();
    }

        public async Task<LoanDetailsDto?> GetByIdAsync(Guid tenantId, int id)
        {
            var loan = await _context.Loans
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId && x.LoanId == id)
                .Include(x => x.Customer)
                .Include(x => x.GoldItems)
                .Include(x => x.Payments)
                .FirstOrDefaultAsync();

            if (loan == null)
                return null;

            var dto = new LoanDetailsDto
            {
                LoanId = loan.LoanId,
                LoanNumber = loan.LoanNumber,
                CustomerId = loan.CustomerId,
                CustomerName = loan.Customer?.Name ?? string.Empty,
                PrincipalAmount = loan.PrincipalAmount,
                InterestPercentage = loan.InterestPercentage,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                Status = loan.Status,
                GoldItems = loan.GoldItems.Select(g => new GoldItemDto
                {
                    GoldItemId = g.GoldItemId,
                    ItemType = (int)g.ItemType,
                    Weight = g.Weight,
                    Purity = g.Purity,
                    Description = g.Description
                }).ToList(),
                Payments = loan.Payments.Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    PrincipalAmount = p.PrincipalAmount,
                    InterestAmount = p.InterestAmount,
                    PenaltyAmount = p.PenaltyAmount,
                    ServiceFee = p.ServiceFee,
                    TotalAmount = p.TotalAmount,
                    PaymentType = p.PaymentType,
                    PaymentDate = p.PaymentDate,
                    Remarks = p.Remarks
                }).ToList()
            };

            return dto;
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByCustomerAsync(Guid tenantId, int customerId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId && x.CustomerId == customerId)
                .Include(x => x.Customer)
                .OrderByDescending(x => x.LoanDate)
                .Select(x => new LoanDto
                {
                    LoanId = x.LoanId,
                    LoanNumber = x.LoanNumber,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer != null ? x.Customer.Name : string.Empty,
                    PrincipalAmount = x.PrincipalAmount,
                    InterestPercentage = x.InterestPercentage,
                    LoanDate = x.LoanDate,
                    DueDate = x.DueDate,
                    Status = x.Status
                })
                .ToListAsync();
        }
}
