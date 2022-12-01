using Library.Entities;
using Microsoft.EntityFrameworkCore;
using static Library.Enums.Enums;

namespace Library.DataPersistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = default!;
    public DbSet<Restaurant> Restaurants { get; set; } = default!;
    public DbSet<RestaurantCategory> RestaurantCategories { get; set; } = default!;
    public DbSet<RestaurantReview> RestaurantReviews { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasMany(r => r.Products)
                  .WithOne(r => r.Restaurant)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(r => r.Orders)
                  .WithOne(r => r.Restaurant)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(r => r.Reviews)
                  .WithOne(r => r.Restaurant)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasMany(r => r.OrderItems)
                  .WithOne(r => r.Order)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Status)
                   .HasConversion(v => v.ToString(), v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
        });
                   

        modelBuilder.Entity<OrderItem>()
                    .HasOne(r => r.Product)
                    .WithMany(r => r.OrderItems)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasMany(r => r.OrderItems)
                  .WithOne(r => r.Product)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Category)
                  .WithMany(r => r.Products)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProductCategory>()
                    .HasMany(r => r.Products)
                    .WithOne(r => r.Category)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RestaurantReview>()
                    .HasOne(r => r.Restaurant)
                    .WithMany(r => r.Reviews)
                    .OnDelete(DeleteBehavior.Cascade);

    }
}
