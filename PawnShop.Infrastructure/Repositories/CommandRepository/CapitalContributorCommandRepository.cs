using Microsoft.EntityFrameworkCore;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.CommandRepository;

public class CapitalContributorCommandRepository : ICapitalContributorCommandRepository
{
    private readonly PawnShopDbContext _context;

    public CapitalContributorCommandRepository(PawnShopDbContext context) => _context = context;

    public Task AddAsync(CapitalContributor entity, CancellationToken cancellationToken = default)
    {
        _context.CapitalContributors.Add(entity);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(CapitalContributor entity, CancellationToken cancellationToken = default)
    {
        _context.CapitalContributors.Update(entity);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = _context.CapitalContributors.FirstOrDefault(x => x.CapitalContributorId == id);
        if (entity != null)
        {
            _context.CapitalContributors.Remove(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }

        return Task.CompletedTask;
    }
}
