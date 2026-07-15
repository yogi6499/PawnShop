using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PawnShop.Application.DTOs;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface ITenantQueryRepository
{
    Task<IEnumerable<TenantDto>> GetActiveTenantsAsync(CancellationToken cancellationToken = default);
}
