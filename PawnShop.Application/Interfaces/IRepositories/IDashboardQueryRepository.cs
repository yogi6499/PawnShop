using PawnShop.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface IDashboardQueryRepository
{
    Task<DashboardDto> GetDashboardAsync(Guid tenantId);
}
