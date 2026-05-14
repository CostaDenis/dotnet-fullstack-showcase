using System.Net.Http.Json;
using Showcase.Core.DTOs.Employee;
using Showcase.Web.Api.Abstractions;

namespace Showcase.Web.Api;

public class EmployeeApi : BaseApi, IEmployeeApi
{

    public EmployeeApi(HttpClient Http) : base(Http)
    {

    }

    HttpResponseMessage response = new();

    public async Task<EmployeeResponse?> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync($"employess/{employeeId}", cancellationToken);
        return await HandleResponse<EmployeeResponse?>(response);
    }

    public async Task<EmployeeResponse> CreateAsync(EmployeeCreateRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PostAsJsonAsync($"employess", request, cancellationToken);
        return await HandleResponse<EmployeeResponse>(response);
    }

    public async Task<EmployeeResponse> UpdateAsync(Guid employeeId, EmployeeUpdateRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PutAsJsonAsync($"employess/{employeeId}", request, cancellationToken);
        return await HandleResponse<EmployeeResponse>(response);
    }
}