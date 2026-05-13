namespace Showcase.Api.Services.Abstractions;

public interface ITokenService
{
    string HashToken(string token);
    bool VerifyToken(string token, string hashedToken);
    string GenerateToken(Guid employeeId, string email);
    bool VerifyAuthToken(string token);
}
