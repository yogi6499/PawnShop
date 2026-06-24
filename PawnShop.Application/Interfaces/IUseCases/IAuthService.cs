namespace PawnShop.Application.Interfaces.IUseCases;

using PawnShop.Application.DTOs;

public interface IAuthService
{
    Task SignupAsync(SignupRequest request, CancellationToken cancellationToken = default);
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task ChangePasswordAsync(int userId, ChangePasswordRequest request, CancellationToken cancellationToken = default);
}
