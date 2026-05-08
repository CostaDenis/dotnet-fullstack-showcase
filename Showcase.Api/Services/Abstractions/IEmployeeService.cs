using Showcase.Core.DTOs.Employee;

namespace Showcase.Api.Services.Abstractions;

public interface IEmployeeService
{
    Task<EmployeeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EmployeeResponse> CreateAsync(EmployeeCreateRequest request, CancellationToken cancellationToken = default);
    Task<EmployeeResponse> UpdateAsync(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancellationToken = default);
}