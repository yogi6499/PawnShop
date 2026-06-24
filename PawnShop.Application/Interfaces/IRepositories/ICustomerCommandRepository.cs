
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface ICustomerCommandRepository
{
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}