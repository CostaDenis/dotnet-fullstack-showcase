using Showcase.Core.DTOs.Auth;

namespace Showcase.Api.Services.Abstractions;

public interface ITokenService
{
    VerifyAuthTokenResponse VerifyAuthToken(string token);
    string HashToken(string token);
    bool VerifyToken(string token, string hashedToken);
    string GenerateToken(Guid employeeId, string email);
}
