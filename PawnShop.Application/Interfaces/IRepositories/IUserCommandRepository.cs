
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface IUserCommandRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdatePasswordAsync(int userId, string newPasswordHash, CancellationToken cancellationToken = default);
}