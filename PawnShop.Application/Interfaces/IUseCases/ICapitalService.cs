using PawnShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IUseCases;

public interface ICapitalService
{
    Task AddCapitalAsync(Guid tenantId, AddCapitalRequest request);
    Task WithdrawCapitalAsync(Guid tenantId, WithdrawCapitalRequest request);

    Task<IEnumerable<CapitalContributorDto>> GetContributorsAsync();
    Task<CapitalContributorDto?> GetContributorByIdAsync(int id);
    Task<int> CreateContributorAsync(CapitalContributorDto dto);
    Task UpdateContributorAsync(int id, CapitalContributorDto dto);
    Task DeleteContributorAsync(int id);
}
