using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Library.Entities.EntitiesConfiguration;

internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.Property(x => x.Name)
               .HasMaxLength(50);

        builder.Property(x => x.Description)
               .HasMaxLength(200);

        builder.Property(x => x.City)
               .HasMaxLength(50);

        builder.Property(x => x.ImageUrl)
               .HasMaxLength(200);

        builder.HasMany(r => r.Products)
               .WithOne(r => r.Restaurant)
               .HasForeignKey(r => r.RestaurantId);

        builder.HasMany(r => r.Orders)
               .WithOne(r => r.Restaurant)
               .HasForeignKey(r => r.RestaurantId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(r => r.Reviews)
               .WithOne(r => r.Restaurant)
               .HasForeignKey(r => r.RestaurantId);

    }
}
