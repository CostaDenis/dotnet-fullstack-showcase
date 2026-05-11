using Microsoft.EntityFrameworkCore;
using Showcase.Api.Data;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;

namespace Showcase.Api.Repositories;

public class ProductRepository(AppDbContext Context) : IProductRepository
{
    public async Task<List<Product>?> GetAllAsync(CancellationToken cancellationToken = default)
        => await Context.Products
            .ToListAsync(cancellationToken);

    public async Task<List<Product>?> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    => await Context.Products
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync(cancellationToken);

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    => await Context.Products
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await Context.Products.AddAsync(product, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        Context.Products.Update(product);
        await Context.SaveChangesAsync(cancellationToken);

        return product;
    }
    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        Context.Products.Remove(product);
        await Context.SaveChangesAsync(cancellationToken);
    }
}