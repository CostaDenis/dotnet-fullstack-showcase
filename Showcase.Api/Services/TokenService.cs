using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Showcase.Api.Services.Abstractions;
using Showcase.Core;

namespace Showcase.Api.Services;

public class TokenService : ITokenService
{
    public string HashToken(string token)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ConfigurationClass.JwtKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(hash);
    }

    public bool VerifyToken(string token, string hashedToken)
    {
        var computedHash = HashToken(token);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromHexString(computedHash),
            Convert.FromHexString(hashedToken)
        );
    }

    public string GenerateToken(Guid employeeId, string email)
    {
        var key = ConfigurationClass.JwtKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, employeeId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            issuer: ConfigurationClass.JwtIssuer,
            audience: ConfigurationClass.JwtAudience,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool VerifyAuthToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = ConfigurationClass.JwtKey;
        var securityKey = Encoding.UTF8.GetBytes(key);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ConfigurationClass.JwtIssuer,
            ValidAudience = ConfigurationClass.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(securityKey),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
