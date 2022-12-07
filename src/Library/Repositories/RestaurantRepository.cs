using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _dbContext;

    private const int maxRestaurantsPageSize = 30;

    public IEnumerable<Restaurant> Restaurants { get; set; }

    public RestaurantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Restaurants = dbContext.Restaurants;
    }

    public async Task AddRestaurantAsync(Restaurant restaurant)
    {
        await _dbContext.Restaurants.AddAsync(restaurant);
    }

    public async Task<bool> DeleteRestaurantAsync(int restaurantId)
    {
        var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
        if(restaurant is null)
        {
            return false;
        }
        _dbContext.Restaurants.Remove(restaurant);
        return true;
    }

    public async Task<Restaurant?> GetRestaurantAsync(int restaurantId)
    {
        var restaurant = await _dbContext.Restaurants.Include(r => r.RestaurantCategory)
                                                     .FirstOrDefaultAsync(r => r.Id == restaurantId);
        return restaurant;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await _dbContext.Restaurants.Include(r => r.RestaurantCategory)
                                                      .ToListAsync();
        return restaurants;
    }

    public async Task<List<Restaurant>> GetRestaurantsAsync
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
            restaurants = restaurants.Where(r => r.RestaurantCategory.Name == category);
        }
        if (!string.IsNullOrEmpty(searchQuery))
        {
            searchQuery = searchQuery.Trim();
            restaurants = restaurants.Where(r => r.Name.Contains(searchQuery) || r.Description.Contains(searchQuery)
            || r.City.Contains(searchQuery) || r.RestaurantCategory.Name.Contains(searchQuery));
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
                                .Include(r => r.RestaurantCategory)
                                .ToListAsync();
    }
}
