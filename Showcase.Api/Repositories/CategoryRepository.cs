using Microsoft.EntityFrameworkCore;
using Showcase.Api.Data;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;

namespace Showcase.Api.Repositories
{
    public class CategoryRepository(AppDbContext Context) : ICategoryRepository
    {
        public async Task<List<Category>?> GetAllAsync(CancellationToken cancellationToken = default)
            => await Context.Categories
                .ToListAsync(cancellationToken);

        public async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        => await Context.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            await Context.Categories.AddAsync(category, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return category;
        }
        public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync(cancellationToken);

            return category;
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken = default)
        {
            Context.Categories.Remove(category);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}