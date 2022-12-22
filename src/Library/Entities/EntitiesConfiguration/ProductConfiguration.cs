using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Library.Entities.EntitiesConfiguration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasMaxLength(50);

        builder.Property(x => x.Description)
               .HasMaxLength(100);

        builder.Property(x => x.ImageUrl)
               .HasMaxLength(200);

        builder.HasOne(r => r.Category)
               .WithMany(r => r.Products)
               .HasForeignKey(r => r.ProductCategoryId);

        builder.HasMany(r => r.ShoppingCartItems)
               .WithOne(r => r.Product)
               .HasForeignKey(r => r.ProductId);
    }
}
