﻿// <auto-generated />
using System;
using Library.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Migrations;

[DbContext(typeof(ApplicationDbContext))]
partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.10")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("Library.Entities.Order", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("Created")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("datetime2");

                b.Property<int>("RestaurantId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("RestaurantId");

                b.ToTable("Orders", (string)null);
            });

        modelBuilder.Entity("Library.Entities.OrderItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<Guid>("OrderId")
                    .HasColumnType("uniqueidentifier");

                b.Property<int>("ProductId")
                    .HasColumnType("int");

                b.Property<int>("ProductQuantity")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("OrderId");

                b.HasIndex("ProductId");

                b.ToTable("OrderItems", (string)null);
            });

        modelBuilder.Entity("Library.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<int>("CategoryId")
                    .HasColumnType("int");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<bool>("InStock")
                    .HasColumnType("bit");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("ProductSize")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RestaurantId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.HasIndex("RestaurantId");

                b.ToTable("Products", (string)null);
            });

        modelBuilder.Entity("Library.Entities.ProductCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Id");

                b.ToTable("ProductCategories", (string)null);
            });

        modelBuilder.Entity("Library.Entities.Restaurant", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Description")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnType("nvarchar(70)");

                b.Property<string>("RestaurantCategoryName")
                    .IsRequired()
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Id");

                b.HasIndex("RestaurantCategoryName");

                b.ToTable("Restaurants", (string)null);
            });

        modelBuilder.Entity("Library.Entities.RestaurantCategory", b =>
            {
                b.Property<string>("Name")
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Name");

                b.ToTable("RestaurantCategories", (string)null);
            });

        modelBuilder.Entity("Library.Entities.RestaurantReview", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Description")
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                b.Property<int>("Rating")
                    .HasColumnType("int");

                b.Property<int>("RestaurantId")
                    .HasColumnType("int");

                b.Property<string>("Title")
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.HasIndex("RestaurantId");

                b.ToTable("RestaurantReviews", (string)null);
            });

        modelBuilder.Entity("Library.Entities.Order", b =>
            {
                b.HasOne("Library.Entities.Restaurant", "Restaurant")
                    .WithMany("Orders")
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Restaurant");
            });

        modelBuilder.Entity("Library.Entities.OrderItem", b =>
            {
                b.HasOne("Library.Entities.Order", "Order")
                    .WithMany("OrderItems")
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Library.Entities.Product", "Product")
                    .WithMany("OrderItems")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Order");

                b.Navigation("Product");
            });

        modelBuilder.Entity("Library.Entities.Product", b =>
            {
                b.HasOne("Library.Entities.ProductCategory", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("Library.Entities.Restaurant", "Restaurant")
                    .WithMany("Products")
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Category");

                b.Navigation("Restaurant");
            });

        modelBuilder.Entity("Library.Entities.Restaurant", b =>
            {
                b.HasOne("Library.Entities.RestaurantCategory", "RestaurantCategory")
                    .WithMany("Restaurants")
                    .HasForeignKey("RestaurantCategoryName")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("RestaurantCategory");
            });

        modelBuilder.Entity("Library.Entities.RestaurantReview", b =>
            {
                b.HasOne("Library.Entities.Restaurant", "Restaurant")
                    .WithMany("Reviews")
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Restaurant");
            });

        modelBuilder.Entity("Library.Entities.Order", b =>
            {
                b.Navigation("OrderItems");
            });

        modelBuilder.Entity("Library.Entities.Product", b =>
            {
                b.Navigation("OrderItems");
            });

        modelBuilder.Entity("Library.Entities.ProductCategory", b =>
            {
                b.Navigation("Products");
            });

        modelBuilder.Entity("Library.Entities.Restaurant", b =>
            {
                b.Navigation("Orders");

                b.Navigation("Products");

                b.Navigation("Reviews");
            });

        modelBuilder.Entity("Library.Entities.RestaurantCategory", b =>
            {
                b.Navigation("Restaurants");
            });
#pragma warning restore 612, 618
    }
}
