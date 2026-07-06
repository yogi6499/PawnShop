using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using System;
using System.Threading.Tasks;

namespace PawnShop.Application.UseCases;

public class DashboardService : IDashboardService
{
    private readonly IDashboardQueryRepository _dashboardQueryRepository;

    public DashboardService(IDashboardQueryRepository dashboardQueryRepository)
    {
        _dashboardQueryRepository = dashboardQueryRepository;
    }

    public Task<DashboardDto> GetDashboardAsync(Guid tenantId)
    {
        return _dashboardQueryRepository.GetDashboardAsync(tenantId);
    }
}
