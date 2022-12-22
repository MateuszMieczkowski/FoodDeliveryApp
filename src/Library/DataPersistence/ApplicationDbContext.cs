using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
