using Microsoft.EntityFrameworkCore;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Domain.Entities;
using PawnShop.Infrastructure.DBContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class CapitalQueryRepository : ICapitalQueryRepository
{
    private readonly PawnShopDbContext _context;

    // constructor
    public CapitalQueryRepository(PawnShopDbContext context) => _context = context;

    public async Task<decimal> GetCurrentCapitalAsync(Guid tenantId)
    {
        return await _context.CapitalTransactions
            .Where(x => x.TenantId == tenantId)
            .OrderByDescending(x => x.CapitalTransactionId)
            .Select(x => x.BalanceAfterTransaction)
            .FirstOrDefaultAsync();
    }

}
