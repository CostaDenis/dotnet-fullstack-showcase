using Microsoft.AspNetCore.Identity;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Auth;
using Showcase.Core.DTOs.Employee;
using Showcase.Core.Exceptions.AuthExceptions;
using Showcase.Core.Exceptions.EmployeeExceptions;

namespace Showcase.Api.Services;

public class AuthService(
    IEmployeeRepository EmployeeRepository,
    ITokenService TokenService
) : IAuthService
{

    public async Task<VerifyResetTokenResponse> ValidatePasswordResetTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var tokenHash = TokenService.HashToken(token);
        var employee = await EmployeeRepository.GetByPasswordTokenHashAsync(tokenHash, cancellationToken)
            ?? throw new EmployeeNotFoundException();

        if (TokenService.VerifyToken(token, employee.PasswordResetTokenHash ?? "") == false
            || employee.PasswordResetTokenExpiration < DateTime.UtcNow)
            return new VerifyResetTokenResponse
            {
                IsValid = false
            };

        return new VerifyResetTokenResponse
        {
            IsValid = true
        }; ;
    }

    public async Task<ConfirmPasswordTokenResponse> ConfirmPasswordTokenAsync(string token, VerifyPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var tokenHash = TokenService.HashToken(token);
        var employee = await EmployeeRepository.GetByPasswordTokenHashAsync(tokenHash, cancellationToken);

        if (employee is null
            || employee.PasswordResetTokenExpiration < DateTime.UtcNow)
            return new ConfirmPasswordTokenResponse
            {
                IsValid = false
            };

        if (employee.VerifyPassword(request.Password))
            throw new PasswordReuseNotAllowedException();

        IPasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
        employee.PasswordResetTokenHash = null;
        employee.PasswordResetTokenExpiration = null;
        employee.SetPassword(request.Password, passwordHasher);

        await EmployeeRepository.UpdateAsync(employee, cancellationToken);

        return new ConfirmPasswordTokenResponse
        {
            IsValid = false
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await EmployeeRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (employee is null
            || !employee.VerifyPassword(request.Password))
            return null;

        var token = TokenService.GenerateToken(employee.Id, employee.Email);

        return new AuthResponse
        {
            Token = token,
            Employee = new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email
            }
        };

    }

    public async Task<ForgotPasswordResponse?> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken = default)
    {
        var employee = await EmployeeRepository.GetByEmailAsync(email, cancellationToken);

        if (employee is null)
            return null;

        var token = Guid.NewGuid().ToString("N");
        employee.PasswordResetTokenHash = TokenService.HashToken(token);
        employee.PasswordResetTokenExpiration = DateTime.UtcNow.AddMinutes(15);

        await EmployeeRepository.UpdateAsync(employee, cancellationToken);

        return new ForgotPasswordResponse
        {
            Token = token
        };
    }

}
