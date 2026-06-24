
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface IUserQueryRepository
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}