using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Api.Models;

namespace Showcase.Api.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("varchar")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Value)
            .HasColumnName("value")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Image)
            .HasColumnName("image")
            .HasColumnType("varchar")
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .HasConstraintName("fk_products_category")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
