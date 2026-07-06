using Microsoft.EntityFrameworkCore;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class DashboardQueryRepository : IDashboardQueryRepository
{
    private readonly PawnShopDbContext _context;

    public DashboardQueryRepository(PawnShopDbContext context) => _context = context;

    public async Task<DashboardDto> GetDashboardAsync(Guid tenantId)
    {
        var availableCapital = await _context.CapitalTransactions
            .Where(x => x.TenantId == tenantId)
            .OrderByDescending(x => x.CapitalTransactionId)
            .Select(x => x.BalanceAfterTransaction)
            .FirstOrDefaultAsync();

        var availableProfit = await _context.ProfitTransactions
            .Where(x => x.TenantId == tenantId)
            .OrderByDescending(x => x.ProfitTransactionId)
            .Select(x => x.BalanceAfterTransaction)
            .FirstOrDefaultAsync();

        var activeLoans = await _context.Loans
            .CountAsync(x => x.TenantId == tenantId && x.Status == LoanStatus.Active);

        var closedLoans = await _context.Loans
            .CountAsync(x => x.TenantId == tenantId && x.Status == LoanStatus.Closed);

        var customers = await _context.Customers
            .CountAsync(x => x.TenantId == tenantId);

        var moneyOnLoan = await _context.Loans
            .Where(x => x.TenantId == tenantId && x.Status == LoanStatus.Active)
            .SumAsync(x => x.PrincipalAmount);

        return new DashboardDto
        {
            AvailableCapital = availableCapital,
            AvailableProfit = availableProfit,
            MoneyOnLoan = moneyOnLoan,
            ActiveLoans = activeLoans,
            ClosedLoans = closedLoans,
            Customers = customers
        };
    }
}
