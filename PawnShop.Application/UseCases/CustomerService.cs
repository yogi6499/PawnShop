
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Application.UseCases;

public class CustomerService : ICustomerService
{
    private readonly ICustomerQueryRepository _query;
    private readonly ICustomerCommandRepository _command;

    public CustomerService(ICustomerQueryRepository query, ICustomerCommandRepository command)
    {
        _query = query;
        _command = command;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _query.GetAllAsync(cancellationToken);
        return entities.Select(MapToDto);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _query.GetByIdAsync(id, cancellationToken);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<int> CreateAsync(CustomerDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Customer
        {
            Name = dto.Name,
            Phone = dto.Phone,
            AadhaarNumber = dto.AadhaarNumber,
            Address = dto.Address,
            Notes = dto.Notes,
            TenantId = dto.TenantId
        };

        await _command.AddAsync(entity, cancellationToken);
        return entity.CustomerId;
    }

    public async Task UpdateAsync(int id, CustomerDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _query.GetByIdAsync(id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException("Customer not found");

        entity.Name = dto.Name;
        entity.Phone = dto.Phone;
        entity.AadhaarNumber = dto.AadhaarNumber;
        entity.Address = dto.Address;
        entity.Notes = dto.Notes;
        entity.TenantId = dto.TenantId;

        await _command.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _command.DeleteAsync(id, cancellationToken);
    }

    private static CustomerDto MapToDto(Customer c) =>
        new CustomerDto
        {
            CustomerId = c.CustomerId,
            Name = c.Name,
            Phone = c.Phone,
            AadhaarNumber = c.AadhaarNumber,
            Address = c.Address,
            Notes = c.Notes,
            TenantId = c.TenantId // keep TenantId mapping if you store it elsewhere; adjust as needed
        };
}