using Library.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Library.DataPersistence;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Address> Addresses { get; set; } = default!;
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
