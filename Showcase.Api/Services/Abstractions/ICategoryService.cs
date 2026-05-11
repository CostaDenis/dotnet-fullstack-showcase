using Showcase.Core.DTOs.Category;

namespace Showcase.Api.Services.Abstractions;

public interface ICategoryService
{
    Task<List<CategoryResponse>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryResponse?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<CategoryResponse> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    Task<CategoryResponse> UpdateAsync(Guid categoryId, CategoryRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);
}
