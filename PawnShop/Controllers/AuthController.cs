using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawnShop.Application.DTOs;
using System.Linq;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PawnShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITenantQueryRepository _tenantQuery;

    public AuthController(IAuthService authService, ITenantQueryRepository tenantQuery)
    {
        _authService = authService;
        _tenantQuery = tenantQuery;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupRequest request)
    {
        try
        {
            await _authService.SignupAsync(request);
            return Ok();
        }
        catch (InvalidOperationException ex) // expected domain error
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return result is null ? Unauthorized() : Ok(result);
        }
        catch (Exception) // allow middleware to handle unexpected cases
        {
            throw;
        }
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        try
        {
            await _authService.ChangePasswordAsync(userId, request);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("tenants")]
    public async Task<IActionResult> GetTenants()
    {
        try
        {
            var tenants = await _authService.GetTenantsAsync();
            return Ok(tenants);
        }
        catch (Exception)
        {
            // Let middleware handle unexpected exceptions to keep behaviour consistent
            throw;
        }
    }
}
