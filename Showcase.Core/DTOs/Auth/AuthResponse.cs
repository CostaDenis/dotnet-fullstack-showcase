using Showcase.Core.DTOs.Employee;

namespace Showcase.Core.DTOs.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public EmployeeResponse Employee { get; set; } = new();
}
