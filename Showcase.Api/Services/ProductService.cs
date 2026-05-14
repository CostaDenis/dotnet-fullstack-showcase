using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Product;
using Showcase.Core.Exceptions.ProductExceptions;

namespace Showcase.Api.Services;

public class ProductService(IProductRepository ProductRepository)
    : IProductService
{
    public async Task<List<ProductResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await ProductRepository.GetAllAsync(cancellationToken);

        if (products is null)
            return null;

        var productsList = new List<ProductResponse>();

        foreach (var product in products)
        {
            productsList.Add(ConvertModelToDTO(product));
        }
        return productsList;
    }

    public async Task<List<ProductResponse>?> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var products = await ProductRepository.GetByCategoryIdAsync(categoryId, cancellationToken);

        if (products is null)
            return null;

        var productsList = new List<ProductResponse>();

        foreach (var product in products)
        {
            productsList.Add(ConvertModelToDTO(product));
        }
        return productsList;
    }
    public async Task<ProductResponse?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await ProductRepository.GetByIdAsync(productId, cancellationToken);

        if (product is null)
            return null;

        return ConvertModelToDTO(product);
    }
    public async Task<ProductResponse> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Value = request.Value,
            Image = request.Image,
            IsActive = request.IsActive,
            CategoryId = request.CategoryId
        };

        var newProduct = await ProductRepository.CreateAsync(product, cancellationToken);

        return ConvertModelToDTO(newProduct);
    }
    public async Task<ProductResponse> UpdateAsync(Guid productId, ProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await ProductRepository.GetByIdAsync(productId, cancellationToken)
            ?? throw new ProductNotFoundException();

        product.Name = request.Name;
        product.Description = request.Description;
        product.Value = request.Value;
        product.Image = request.Image;
        product.CreatedAt = request.CreatedAt;
        product.IsActive = request.IsActive;
        product.CategoryId = request.CategoryId;

        var updatedProduct = await ProductRepository.UpdateAsync(product, cancellationToken);

        return ConvertModelToDTO(updatedProduct);
    }
    public async Task DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await ProductRepository.GetByIdAsync(productId, cancellationToken)
            ?? throw new ProductNotFoundException();

        await ProductRepository.DeleteAsync(product, cancellationToken);
    }

    public ProductResponse ConvertModelToDTO(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Value = product.Value,
            Image = product.Image,
            CreatedAt = product.CreatedAt,
            IsActive = product.IsActive,
            CategoryId = product.CategoryId
        };
    }
}
