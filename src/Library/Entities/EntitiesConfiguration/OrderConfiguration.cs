using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Library.Enums.Enums;

namespace Library.Entities.EntitiesConfiguration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Created)
               .HasDefaultValueSql("getdate()");

        builder.HasMany(x => x.OrderItems)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId);

        builder.OwnsOne(x => x.Address, x =>
        {
            x.Property(r => r.Street).HasMaxLength(40);
            x.Property(r => r.City).HasMaxLength(40);
            x.Property(r => r.ZipCode).HasMaxLength(10);
        });

        builder.Property(e => e.Status)
               .HasConversion(v => v.ToString(), v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
    }
}
