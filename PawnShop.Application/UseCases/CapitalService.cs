using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Domain.Entities;
using PawnShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawnShop.Application.UseCases;

public class CapitalService : ICapitalService
{
    private readonly ICapitalQueryRepository _capitalQueryRepository;
    private readonly ICapitalCommandRepository _capitalCommandRepository;
    private readonly ICapitalContributorQueryRepository _contribQuery;
    private readonly ICapitalContributorCommandRepository _contribCommand;

    public CapitalService(
        ICapitalQueryRepository capitalQueryRepository,
        ICapitalCommandRepository capitalCommandRepository,
        ICapitalContributorQueryRepository contribQuery,
        ICapitalContributorCommandRepository contribCommand)
    {
        _capitalQueryRepository = capitalQueryRepository;
        _capitalCommandRepository = capitalCommandRepository;
        _contribQuery = contribQuery;
        _contribCommand = contribCommand;
    }

    public async Task AddCapitalAsync(Guid tenantId, AddCapitalRequest request)
    {
        var currentCapital = await _capitalQueryRepository.GetCurrentCapitalAsync(tenantId);

        var transaction = new CapitalTransaction
        {
            TenantId = tenantId,
            CapitalContributorId = request.CapitalContributorId,
            TransactionType = CapitalTransactionType.CapitalAdded,
            Amount = request.Amount,
            BalanceAfterTransaction = currentCapital + request.Amount,
            TransactionDate = DateTime.UtcNow,
            Remarks = request.Remarks
        };

        await _capitalCommandRepository.AddTransactionAsync(transaction);
    }

    public async Task WithdrawCapitalAsync(Guid tenantId, WithdrawCapitalRequest request)
    {
        var currentCapital = await _capitalQueryRepository.GetCurrentCapitalAsync(tenantId);

        if (currentCapital < request.Amount)
            throw new InvalidOperationException("Insufficient capital");

        var transaction = new CapitalTransaction
        {
            TenantId = tenantId,
            TransactionType = CapitalTransactionType.CapitalWithdrawn,
            Amount = request.Amount,
            BalanceAfterTransaction = currentCapital - request.Amount,
            TransactionDate = DateTime.UtcNow,
            Remarks = request.Remarks
        };

        await _capitalCommandRepository.AddTransactionAsync(transaction);
    }

    public async Task<IEnumerable<CapitalContributorDto>> GetContributorsAsync()
    {
        var entities = await _contribQuery.GetAllAsync();
        return entities.Select(x => new CapitalContributorDto
        {
            CapitalContributorId = x.CapitalContributorId,
            Name = x.Name,
            Phone = x.Phone,
            IsActive = x.IsActive,
            TenantId = x.TenantId
        });
    }

    public async Task<CapitalContributorDto?> GetContributorByIdAsync(int id)
    {
        var entity = await _contribQuery.GetByIdAsync(id);
        if (entity == null) return null;
        return new CapitalContributorDto
        {
            CapitalContributorId = entity.CapitalContributorId,
            Name = entity.Name,
            Phone = entity.Phone,
            IsActive = entity.IsActive,
            TenantId = entity.TenantId
        };
    }

    public async Task<int> CreateContributorAsync(CapitalContributorDto dto)
    {
        var entity = new CapitalContributor
        {
            Name = dto.Name,
            Phone = dto.Phone,
            IsActive = dto.IsActive,
            TenantId = dto.TenantId
        };

        await _contribCommand.AddAsync(entity);
        return entity.CapitalContributorId;
    }

    public async Task UpdateContributorAsync(int id, CapitalContributorDto dto)
    {
        var entity = await _contribQuery.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Contributor not found");

        entity.Name = dto.Name;
        entity.Phone = dto.Phone;
        entity.IsActive = dto.IsActive;
        entity.TenantId = dto.TenantId;

        await _contribCommand.UpdateAsync(entity);
    }

    public async Task DeleteContributorAsync(int id)
    {
        await _contribCommand.DeleteAsync(id);
    }
}
