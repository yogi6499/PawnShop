using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IUseCases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PawnShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
}
