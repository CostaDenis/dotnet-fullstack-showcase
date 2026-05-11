using Showcase.Api.Exceptions.CategoryExceptions;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Category;

namespace Showcase.Api.Services;

public class CategoryService(ICategoryRepository CategoryRepository) : ICategoryService
{
    public async Task<List<CategoryResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await CategoryRepository.GetAllAsync(cancellationToken);

        if (categories is null)
            return null;

        var categoriesList = new List<CategoryResponse>();

        foreach (var category in categories)
        {
            categoriesList.Add(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        return categoriesList;
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var category = await CategoryRepository.GetByIdAsync(categoryId, cancellationToken);

        if (category is null)
            return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryResponse> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = new Category
        {
            Name = request.Name
        };

        await CategoryRepository.CreateAsync(category, cancellationToken);

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryResponse> UpdateAsync(Guid categoryId, CategoryRequest request, CancellationToken cancellationToken = default)
    {
        var category = await CategoryRepository.GetByIdAsync(categoryId, cancellationToken)
            ?? throw new CategoryNotFoundException();

        category.Name = request.Name;

        var updatedCategory = await CategoryRepository.UpdateAsync(category, cancellationToken);

        return new CategoryResponse
        {
            Id = category.Id,
            Name = updatedCategory.Name
        };
    }

    public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var category = await CategoryRepository.GetByIdAsync(categoryId, cancellationToken)
            ?? throw new CategoryNotFoundException();

        await CategoryRepository.DeleteAsync(category, cancellationToken);
    }
}
