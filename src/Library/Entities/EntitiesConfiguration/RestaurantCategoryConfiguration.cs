using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Entities.EntitiesConfiguration;

internal class RestaurantCategoryConfiguration : IEntityTypeConfiguration<RestaurantCategory>
{
    public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
    {
        builder.HasKey(x => x.Name);
        builder.Property(x => x.Name)
               .HasMaxLength(20);

        builder.HasMany(x => x.Restaurants)
               .WithOne(x => x.RestaurantCategory)
               .HasForeignKey(x => x.RestaurantCategoryName);
    }
}
