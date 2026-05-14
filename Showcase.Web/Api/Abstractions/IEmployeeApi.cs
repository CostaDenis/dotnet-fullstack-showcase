using Showcase.Core.DTOs.Employee;

namespace Showcase.Web.Api.Abstractions;

public interface IEmployeeApi
{
    Task<EmployeeResponse?> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<EmployeeResponse> CreateAsync(EmployeeCreateRequest request, CancellationToken cancellationToken = default);
    Task<EmployeeResponse> UpdateAsync(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancellationToken = default);
}