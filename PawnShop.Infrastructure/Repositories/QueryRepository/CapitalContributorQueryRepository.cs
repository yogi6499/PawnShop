using Microsoft.EntityFrameworkCore;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class CapitalContributorQueryRepository : ICapitalContributorQueryRepository
{
    private readonly PawnShopDbContext _context;

    public CapitalContributorQueryRepository(PawnShopDbContext context) => _context = context;

    public Task<IEnumerable<CapitalContributor>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _context.CapitalContributors.AsNoTracking().ToListAsync(cancellationToken).ContinueWith(t => (IEnumerable<CapitalContributor>)t.Result, cancellationToken);

    public Task<CapitalContributor?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.CapitalContributors.AsNoTracking().FirstOrDefaultAsync(x => x.CapitalContributorId == id, cancellationToken);

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default) =>
        _context.CapitalContributors.AnyAsync(x => x.CapitalContributorId == id, cancellationToken);
}
