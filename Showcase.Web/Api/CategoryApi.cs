using System.Net.Http.Json;
using Showcase.Core.DTOs.Category;
using Showcase.Web.Api.Abstractions;

namespace Showcase.Web.Api;

public class CategoryApi : BaseApi, ICategoryApi
{

    public CategoryApi(HttpClient Http) : base(Http)
    {

    }
    HttpResponseMessage response = new();

    public async Task<List<CategoryResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync("categories", cancellationToken);
        return await HandleResponse<List<CategoryResponse>?>(response);
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync($"categories/{categoryId}", cancellationToken);
        return await HandleResponse<CategoryResponse?>(response);
    }

    public async Task<CategoryResponse> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PostAsJsonAsync("categories", request, cancellationToken);
        return await HandleResponse<CategoryResponse>(response);
    }

    public async Task<CategoryResponse> UpdateAsync(Guid categoryId, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PutAsJsonAsync($"categories/{categoryId}", request, cancellationToken);
        return await HandleResponse<CategoryResponse>(response);
    }

    public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        response = await _Http.DeleteAsync($"categories/{categoryId}", cancellationToken);
        await HandleResponse(response);
    }

}
