using System.Net.Http.Json;
using Showcase.Core.DTOs.Auth;
using Showcase.Web.Api.Abstractions;

namespace Showcase.Web.Api
{
    public class AuthApi : BaseApi, IAuthApi
    {

        public AuthApi(HttpClient Http) : base(Http)
        {

        }

        HttpResponseMessage response = new();

        public async Task<VerifyResetTokenResponse> ValidatePasswordResetTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            response = await _Http.GetAsync($"auth/password-reset?token={Uri.EscapeDataString(token)}");
            return await HandleResponse<VerifyResetTokenResponse>(response);
        }

        public async Task<VerifyAuthTokenResponse> VerifyAuthToken(string token, CancellationToken cancellationToken = default)
        {
            response = await _Http.GetAsync($"auth/verify-auth-token?token={Uri.EscapeDataString(token)}");
            return await HandleResponse<VerifyAuthTokenResponse>(response);
        }

        public async Task<ConfirmPasswordTokenResponse> ConfirmPasswordTokenAsync(string token, VerifyPasswordRequest request, CancellationToken cancellationToken = default)
        {
            response = await _Http
                .PostAsJsonAsync($"auth/password-reset/confirm?token={Uri.EscapeDataString(token)}", request, cancellationToken);
            return await HandleResponse<ConfirmPasswordTokenResponse>(response);
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            response = await _Http.PostAsJsonAsync("auth/login", request, cancellationToken);
            return await HandleResponse<AuthResponse?>(response);
        }

        public async Task<ForgotPasswordResponse?> GeneratePasswordResetTokenAsync(EmailRequest request, CancellationToken cancellationToken = default)
        {
            response = await _Http.PostAsJsonAsync("auth/forgot-password", request, cancellationToken);
            return await HandleResponse<ForgotPasswordResponse?>(response);
        }

    }
}