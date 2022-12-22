using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Entities.EntitiesConfiguration
{
    internal class RestaurantReviewConfiguration : IEntityTypeConfiguration<RestaurantReview>
    {
        public void Configure(EntityTypeBuilder<RestaurantReview> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn();

            builder.Property(x => x.Title)
                   .HasMaxLength(50);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);
        }
    }
}
