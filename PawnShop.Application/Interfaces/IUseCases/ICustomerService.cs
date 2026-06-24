
using PawnShop.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IUseCases;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CustomerDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CustomerDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}