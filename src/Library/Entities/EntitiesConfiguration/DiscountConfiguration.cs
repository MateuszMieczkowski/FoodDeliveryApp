using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Entities.EntitiesConfiguration;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn();
        
        builder.Property(x => x.DiscountCode)
               .HasMaxLength(20);

        builder.HasMany(x => x.Orders)
               .WithOne(x => x.Discount)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(x=>x.DiscountId);
    }
}

public class PercentageDiscountConfiguration : IEntityTypeConfiguration<PercentageDiscount>
{
    public void Configure(EntityTypeBuilder<PercentageDiscount> builder)
    {
        builder.HasBaseType<Discount>();

        builder.HasCheckConstraint("Percentage_Check", "Percentage BETWEEN 0.0 AND 1.0");
    }
}

public class AmountDiscountConfiguration : IEntityTypeConfiguration<AmountDiscount>
{
    public void Configure(EntityTypeBuilder<AmountDiscount> builder)
    {
        builder.HasBaseType<Discount>();
    }
}