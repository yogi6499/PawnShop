using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IUseCases;
using System;
using System.Threading.Tasks;

namespace PawnShop.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CapitalController : ControllerBase
{
    private readonly ICapitalService _capitalService;

    public CapitalController(ICapitalService capitalService) => _capitalService = capitalService;

    [HttpPost("add/{tenantId}")]
    public async Task<IActionResult> AddCapital(Guid tenantId, AddCapitalRequest request)
    {
        await _capitalService.AddCapitalAsync(tenantId, request);
        return Ok();
    }

    [HttpPost("withdraw/{tenantId}")]
    public async Task<IActionResult> WithdrawCapital(Guid tenantId, WithdrawCapitalRequest request)
    {
        try
        {
            await _capitalService.WithdrawCapitalAsync(tenantId, request);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Capital contributor CRUD
    [HttpGet("contributors")]
    public async Task<IActionResult> GetContributors()
    {
        var list = await _capitalService.GetContributorsAsync();
        return Ok(list);
    }

    [HttpGet("contributors/{id}")]
    public async Task<IActionResult> GetContributor(int id)
    {
        var dto = await _capitalService.GetContributorByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost("contributors")]
    public async Task<IActionResult> CreateContributor(CapitalContributorDto dto)
    {
        var id = await _capitalService.CreateContributorAsync(dto);
        return CreatedAtAction(nameof(GetContributor), new { id }, null);
    }

    [HttpPut("contributors/{id}")]
    public async Task<IActionResult> UpdateContributor(int id, CapitalContributorDto dto)
    {
        await _capitalService.UpdateContributorAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("contributors/{id}")]
    public async Task<IActionResult> DeleteContributor(int id)
    {
        await _capitalService.DeleteContributorAsync(id);
        return NoContent();
    }
}
