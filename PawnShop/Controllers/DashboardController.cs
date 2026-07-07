using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawnShop.Application.Interfaces.IUseCases;
using System;

namespace PawnShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("GetDashboard/{tenantId}")]
        public async Task<IActionResult> GetDashboard(Guid tenantId)
        {
            var dto = await _dashboardService.GetDashboardAsync(tenantId);
            return Ok(dto);
        }
    }
}
