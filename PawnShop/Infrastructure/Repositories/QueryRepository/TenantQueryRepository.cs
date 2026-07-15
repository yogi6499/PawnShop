using Microsoft.EntityFrameworkCore;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class TenantQueryRepository : ITenantQueryRepository
{
    private readonly PawnShopDbContext _context;

    public TenantQueryRepository(PawnShopDbContext context) => _context = context;

    public Task<IEnumerable<TenantDto>> GetActiveTenantsAsync(CancellationToken cancellationToken = default) =>
        _context.Tenants
            .Where(t => t.IsActive)
            .Select(t => new TenantDto
            {
                TenantId = t.Id,
                Code = t.Code,
                Name = t.Name
            })
            .ToListAsync(cancellationToken)
            .ContinueWith(t => (IEnumerable<TenantDto>)t.Result, cancellationToken);
}
