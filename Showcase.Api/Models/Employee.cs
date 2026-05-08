using Microsoft.AspNetCore.Identity;

namespace Showcase.Api.Models;

public class Employee
{
    private static readonly PasswordHasher<Employee> passwordHasher = new();

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public Employee()
    { }

    public bool VerifyPassword(string password)
    {
        var result = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }

    public string PasswordHasher(string password)
        => passwordHasher.HashPassword(this, password);
}
