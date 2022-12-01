using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _dbContext;

    private const int maxRestaurantsPageSize = 30;
    public RestaurantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRestaurantAsync(Restaurant restaurant)
    {
        await _dbContext.Restaurants.AddAsync(restaurant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRestaurantAsync(Restaurant restaurant)
    {
        _dbContext.Restaurants.Remove(restaurant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Restaurant?> GetRestaurantAsync(int id)
    {
        var restaurant = await _dbContext.Restaurants.Include(r => r.Products)
                                                     .Include(r => r.Orders)
                                                     .Include(r => r.Reviews)
                                                     .Include(r => r.RestaurantCategory)
                                                     .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await _dbContext.Restaurants.Include(r => r.Products)
                                                      .Include(r => r.Orders)
                                                      .Include(r => r.Reviews)
                                                      .Include(r => r.RestaurantCategory)
                                                      .ToListAsync();
        return restaurants;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync
        (string? name, string? city, string? category, string? searchQuery, int pageNumber = 1, int pageSize = 15)
    {
        var restaurants = _dbContext.Restaurants as IQueryable<Restaurant>;
        if (!string.IsNullOrEmpty(name))
        {
            name = name.Trim();
            restaurants = restaurants.Where(r => r.Name == name);
        }
        if (!string.IsNullOrEmpty(city))
        {
            city = city.Trim();
            restaurants = restaurants.Where(r => r.City == city);
        }
        if (!string.IsNullOrEmpty(category))
        {
            category = category.Trim();
            restaurants = restaurants.Where(r => r.RestaurantCategoryName == category);
        }
        if (!string.IsNullOrEmpty(searchQuery))
        {
            searchQuery = searchQuery.Trim().ToLower();
            restaurants = restaurants.Where(r => r.Name.Contains(searchQuery) || r.Description.Contains(searchQuery)
            || r.City.Contains(searchQuery) || r.RestaurantCategoryName.Contains(searchQuery));
        }
        if(pageSize > maxRestaurantsPageSize)
        {
            pageSize = maxRestaurantsPageSize;
        }
        if(pageSize == 0)
        {
            pageSize = 15;
        }
        if(pageNumber == 0)
        {
            pageNumber = 1;
        }

        return await restaurants.Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .Include(r => r.Products)
                                .Include(r => r.Orders)
                                .Include(r => r.Reviews)
                                .Include(r => r.RestaurantCategory)
                                .ToListAsync();
    }
}
