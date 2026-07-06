using PawnShop.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface ICapitalQueryRepository
{
    Task<decimal> GetCurrentCapitalAsync(Guid tenantId);
}

public interface ICapitalCommandRepository
{
    Task AddTransactionAsync(CapitalTransaction transaction);
}
