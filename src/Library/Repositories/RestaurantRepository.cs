using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IEnumerable<Restaurant> AllRestaurants { get; set; }

    public RestaurantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        AllRestaurants = dbContext.Restaurants.Include(r => r.Products)
                                              .Include(r => r.Orders)
                                              .Include(r => r.Reviews)
                                              .Include(r => r.RestaurantCategory);
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
        var restaurant = await _dbContext.Restaurants.FindAsync(id);
        return restaurant;
    }
}
