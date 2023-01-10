using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities.EntitiesConfiguration;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Street)
            .HasMaxLength(40);

        builder.Property(x => x.City)
            .HasMaxLength(40);
            
        builder.Property(x => x.ZipCode)
            .HasMaxLength(10);

    }
}