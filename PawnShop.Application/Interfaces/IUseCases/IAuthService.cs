using System.Collections.Generic;
using PawnShop.Application.DTOs;

namespace PawnShop.Application.Interfaces.IUseCases;

public interface IAuthService
{
    Task SignupAsync(SignupRequest request, CancellationToken cancellationToken = default);
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task ChangePasswordAsync(int userId, ChangePasswordRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<TenantDto>> GetTenantsAsync(CancellationToken cancellationToken = default);
}
