using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Entities.EntitiesConfiguration;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();

        builder.HasOne(r => r.Product)
               .WithMany(r => r.OrderItems)
               .HasForeignKey(r => r.ProductId);
    }
}
