using Showcase.Core.DTOs.Auth;

namespace Showcase.Web.Api.Abstractions;

public interface IAuthApi
{
    Task<VerifyResetTokenResponse> ValidatePasswordResetTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<VerifyAuthTokenResponse> VerifyAuthToken(string token, CancellationToken cancellationToken = default);
    Task<ConfirmPasswordTokenResponse> ConfirmPasswordTokenAsync(string token, VerifyPasswordRequest request, CancellationToken cancellationToken = default);
    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<ForgotPasswordResponse?> GeneratePasswordResetTokenAsync(EmailRequest request, CancellationToken cancellationToken = default);

}