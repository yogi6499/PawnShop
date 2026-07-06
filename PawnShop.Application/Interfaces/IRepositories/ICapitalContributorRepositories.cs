using PawnShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface ICapitalContributorQueryRepository
{
    Task<IEnumerable<CapitalContributor>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
    Task<CapitalContributor?> GetByIdAsync(int id, System.Threading.CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, System.Threading.CancellationToken cancellationToken = default);
}

public interface ICapitalContributorCommandRepository
{
    Task AddAsync(CapitalContributor entity, System.Threading.CancellationToken cancellationToken = default);
    Task UpdateAsync(CapitalContributor entity, System.Threading.CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, System.Threading.CancellationToken cancellationToken = default);
}
