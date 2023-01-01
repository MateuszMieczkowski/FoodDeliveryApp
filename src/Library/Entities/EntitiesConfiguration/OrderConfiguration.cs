﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
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

        builder.Property(e => e.Status)
               .HasConversion(v => v.ToString(), v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
    }
}
