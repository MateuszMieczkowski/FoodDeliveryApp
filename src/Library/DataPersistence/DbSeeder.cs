using Bogus;
using Bogus.Extensions;
using Library.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static Library.Enums.Enums;

namespace Library.DataPersistence;

public class DbSeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    public DbSeeder(ApplicationDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public void Seed()
    {
        if (!_dbContext.ProductCategories.Any())
        {
            var categories = GetProductCategories();
            _dbContext.ProductCategories.AddRange(categories);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.RestaurantCategories.Any())
        {
            var categories = GetRestaurantCategories();
            _dbContext.RestaurantCategories.AddRange(categories);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.Restaurants.Any())
        {
            var restaurants = GetRestaurants();
            _dbContext.AddRange(restaurants);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.Products.Any())
        {
            var products = GetProducts();
            _dbContext.Products.AddRange(products);
            _dbContext.SaveChanges();
        }

       
        if(! _dbContext.RestaurantReviews.Any())
        {
            var reviews = GetRestaurantReviews();
            _dbContext.RestaurantReviews.AddRange(reviews);
            _dbContext.SaveChanges();
        }

        if (!_dbContext.Roles.Any())
        {
            var roles = GetRoles();
            foreach (var identityRole in roles)
            {
                _roleManager.CreateAsync(identityRole).GetAwaiter().GetResult();
            }
        }

        if (!_userManager.GetUsersInRoleAsync("admin").Result.Any())
        {
            var adminUser = new User
            {
                Email = "admin@fooddeliveryapp.com",
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin"
            };
            _userManager.CreateAsync(adminUser, "administrator").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, "ADMIN").GetAwaiter().GetResult();
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
				new Claim(ClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}")
			};
			var roles = _userManager.GetRolesAsync(adminUser).GetAwaiter().GetResult();
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
            _userManager.AddClaimsAsync(adminUser, claims);
		}
    }

    private static ProductCategory[] GetProductCategories()
    {
        var categories = new ProductCategory[] {
            new() { Name= "Pizza"},
            new() { Name= "Pasta"},
            new() { Name= "Meat"},
            new() { Name= "Burgers"},
            new() { Name= "Sushi"},
            new() { Name= "Snacks"},
            new() { Name= "Starters"}
        };
        return categories;
    }

    private static RestaurantCategory[] GetRestaurantCategories()
    {
        var categories = new RestaurantCategory[] {
            new() { Name= "Pizza"},
            new() { Name= "Pasta"},
            new() { Name= "Meat"},
            new() { Name= "Burgers"},
            new() { Name= "Sushi"},
            new() { Name= "Asian"},
            new() { Name= "American"},
            new() { Name= "Polish"},
            new() { Name= "Italian"},
        };
        return categories;
    }

    private Restaurant[] GetRestaurants()
    {
        var faker = new Faker<Restaurant>();
        faker.RuleFor(r => r.Name, f => f.Company.CompanyName())
            .RuleFor(r => r.Description, f => f.Company.CatchPhrase())
            .RuleFor(r => r.City, f => f.Address.City())
            .RuleFor(r => r.RestaurantCategory, f => f.PickRandom(_dbContext.RestaurantCategories.ToArray()))
            .RuleFor(r => r.ImageUrl, f => f.Image.LoremFlickrUrl(keywords: "restaurant"));

        return faker.Generate(200).ToArray();
    }

    private Product[] GetProducts()
    {
        var faker = new Faker<Product>();
        faker.RuleFor(r => r.Name, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(2))
            .RuleFor(r => r.Price, f => f.Random.Decimal2() * 50)
            .RuleFor(r => r.InStock, f => f.Random.Bool())
            .RuleFor(r => r.Category, f => f.PickRandom(_dbContext.ProductCategories.ToArray()))
            .RuleFor(r => r.Restaurant, f => f.PickRandom(_dbContext.Restaurants.ToArray()))
            .RuleFor(r => r.ImageUrl, f => f.Image.LoremFlickrUrl(keywords: "food"));

        return faker.Generate(1000).ToArray();
    }

    private RestaurantReview[] GetRestaurantReviews()
    {
        var faker = new Faker<RestaurantReview>();

        faker.RuleFor(r => r.Title, f => f.Lorem.Sentence(1))
            .RuleFor(r => r.Description, f => f.Lorem.Sentence(5))
            .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
            .RuleFor(r => r.Restaurant, f => f.PickRandom(_dbContext.Restaurants.ToArray()));

        return faker.Generate(10000).ToArray();
    }


    private Order[] GetOrders()
    {
        var faker = new Faker<Order>();

        faker.RuleFor(r => r.Created, f => f.Date.Recent(30))
            .RuleFor(r => r.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(r => r.Restaurant, f => f.PickRandom(_dbContext.Restaurants.ToArray()));

        var orders = faker.Generate(1000);

        orders.ForEach(r =>
        {
            r.OrderItems = GetOrderItems(r.Restaurant);
            r.OrderItems.ToList().ForEach(x => x.Order = r);
            r.Total = r.OrderItems.Where(x => x.Order == r).Sum(x => x.ProductQuantity * x.Product.Price);
        });

        return orders.ToArray();
    }

    private OrderItem[] GetOrderItems(Restaurant restaurant)
    {
        var products = _dbContext.Products.Where(r => r.RestaurantId == restaurant.Id).ToArray();

        var faker = new Faker<OrderItem>();
        faker.RuleFor(r => r.Product, f => f.PickRandom(products))
            .RuleFor(r => r.ProductQuantity, f => f.Random.Int(1, 5));

        return faker.GenerateBetween(1, 6).ToArray();
    }

    private static IdentityRole<Guid>[] GetRoles()
    {
        IdentityRole<Guid>[] roles = new[]
        {
            new IdentityRole<Guid>("user"),
            new IdentityRole<Guid>("manager"),
            new IdentityRole<Guid>("admin")
        };

        return roles;
    }

}