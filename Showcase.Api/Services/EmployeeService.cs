using Microsoft.AspNetCore.Identity;
using Showcase.Api.Exceptions.EmployeeExceptions;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Employee;

namespace Showcase.Api.Services;

public class EmployeeService(IEmployeeRepository EmployeeRepository) : IEmployeeService
{
    public async Task<EmployeeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var employee = await EmployeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee is null)
            return new EmployeeResponse();

        return new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email
        };
    }

    public async Task<EmployeeResponse> CreateAsync(EmployeeCreateRequest request, CancellationToken cancellationToken = default)
    {
        var existsEmployee = await EmployeeRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existsEmployee is not null)
            throw new Exception("Email já registrado!");

        IPasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
        var employee = new Employee
        {
            Name = request.Name,
            Email = request.Email
        };
        employee.SetPassword(request.Password, passwordHasher);

        var newEmployee = await EmployeeRepository.CreateAsync(employee, cancellationToken);

        return new EmployeeResponse
        {
            Id = newEmployee.Id,
            Name = newEmployee.Name,
            Email = newEmployee.Email
        };
    }

    public async Task<EmployeeResponse> UpdateAsync(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await EmployeeRepository.GetByIdAsync(employeeId, cancellationToken)
            ?? throw new EmployeeNotFoundException();

        var existsEmployee = await EmployeeRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existsEmployee is not null
            && existsEmployee.Id != employee.Id)
            throw new EmailAlreadyUsedException();

        employee.Name = request.Name;
        employee.Email = request.Email;

        var updatedEmployee = await EmployeeRepository.UpdateAsync(employee, cancellationToken);

        return new EmployeeResponse
        {
            Id = updatedEmployee.Id,
            Name = updatedEmployee.Name,
            Email = updatedEmployee.Email
        };
    }
}
