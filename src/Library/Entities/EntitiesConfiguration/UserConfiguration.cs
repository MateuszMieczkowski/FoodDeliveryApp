using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Entities.EntitiesConfiguration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.Property(x => x.FirstName)
               .HasMaxLength(30);

        builder.Property(x => x.LastName)
               .HasMaxLength(40);

        builder.HasMany(x => x.Address)
               .WithOne(x => x.User)
               .HasForeignKey(x=>x.UserId);

        builder.HasMany(x => x.Orders)
               .WithOne(x => x.User)
               .HasForeignKey(x=>x.UserId);

    }
}
