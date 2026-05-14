using System.Net.Http.Json;
using Showcase.Core.DTOs.Product;
using Showcase.Web.Api.Abstractions;

namespace Showcase.Web.Api;

public class ProductApi : BaseApi, IProductApi
{

    public ProductApi(HttpClient Http) : base(Http)
    {

    }

    HttpResponseMessage response = new();

    public async Task<List<ProductResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync("products", cancellationToken);
        return await HandleResponse<List<ProductResponse>?>(response);
    }

    public async Task<List<ProductResponse>?> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync($"products/categories/{categoryId}", cancellationToken);
        return await HandleResponse<List<ProductResponse>?>(response);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        response = await _Http.GetAsync($"products/{productId}", cancellationToken);
        return await HandleResponse<ProductResponse?>(response);
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PostAsJsonAsync("products", request, cancellationToken);
        return await HandleResponse<ProductResponse>(response);
    }

    public async Task<ProductResponse> UpdateAsync(Guid productId, ProductRequest request, CancellationToken cancellationToken = default)
    {
        response = await _Http.PutAsJsonAsync($"products/{productId}", request, cancellationToken);
        return await HandleResponse<ProductResponse>(response);
    }

    public async Task DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        response = await _Http.DeleteAsync($"products/{productId}", cancellationToken);
        await HandleResponse(response);
    }

}