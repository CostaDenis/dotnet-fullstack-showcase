using Showcase.Core.DTOs.Auth;

namespace Showcase.Api.Services.Abstractions;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<string?> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ValidatePasswordResetTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<bool> ConfirmPasswordTokenAsync(string token, VerifyPasswordRequest request, CancellationToken cancellationToken = default);
}
