using Showcase.Core.DTOs.Product;

namespace Showcase.Web.Api.Abstractions;

public interface IProductApi
{
    Task<List<ProductResponse>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<ProductResponse>?> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<ProductResponse?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<ProductResponse> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default);
    Task<ProductResponse> UpdateAsync(Guid productId, ProductRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
}