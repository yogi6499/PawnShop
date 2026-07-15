using BCrypt.Net;
using System.Collections.Generic;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace PawnShop.Application.UseCases;

public class AuthService : IAuthService
{
    private readonly IUserQueryRepository _userQuery;
    private readonly IUserCommandRepository _userCommand;
    private readonly IJwtService _jwtService;
    private readonly ITenantQueryRepository _tenantQuery;

    public AuthService(IUserQueryRepository userQuery, IUserCommandRepository userCommand, IJwtService jwtService, ITenantQueryRepository tenantQuery)
    {
        _userQuery = userQuery;
        _userCommand = userCommand;
        _jwtService = jwtService;
        _tenantQuery = tenantQuery;
    }

    public async Task SignupAsync(SignupRequest request, CancellationToken cancellationToken = default)
    {
        var exists = await _userQuery.ExistsByEmailAsync(request.Email, cancellationToken);
        if (exists) throw new InvalidOperationException("User already exists");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            TenantId = request.TenantId,
            Role = request.Role,
            IsActive = true,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _userCommand.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<TenantDto>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        return await _tenantQuery.GetActiveTenantsAsync(cancellationToken);
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userQuery.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null) return null;

        var valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!valid) return null;

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Email = user.Email!,
            Role = user.Role.ToString(),
            Expiry = DateTime.UtcNow.AddDays(1)
        };
    }

    public async Task ChangePasswordAsync(int userId, ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userQuery.GetByIdAsync(userId, cancellationToken);
        if (user == null) throw new UnauthorizedAccessException();

        var valid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
        if (!valid) throw new InvalidOperationException("Current password incorrect");

        var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _userCommand.UpdatePasswordAsync(userId, newHash, cancellationToken);
    }
}