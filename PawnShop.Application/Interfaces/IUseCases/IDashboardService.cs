using PawnShop.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IUseCases;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync(Guid tenantId);
}
