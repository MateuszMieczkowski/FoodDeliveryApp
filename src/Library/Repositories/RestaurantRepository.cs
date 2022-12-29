using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IQueryable<Restaurant> Restaurants { get; set; }

    public RestaurantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Restaurants = dbContext.Restaurants;
    }

    public async Task AddRestaurantAsync(Restaurant restaurant)
    {
        await _dbContext.Restaurants.AddAsync(restaurant);
    }

    public async Task DeleteRestaurantAsync(Restaurant restaurant)
    {
        //including orders to be deleted by ef core because of clientCascade deletion
        var restaurantToDelete = await _dbContext.Restaurants.Include(r => r.Orders).SingleOrDefaultAsync(r => r.Id == restaurant.Id);
        _dbContext.Restaurants.Remove(restaurantToDelete!);
    }

    public async Task<Restaurant?> GetRestaurantAsync(int restaurantId)
    {
        var restaurant = await _dbContext.Restaurants.FindAsync(restaurantId);
        return restaurant;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await _dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<int> GetRestaurantsCount()
    {
        return await _dbContext.Restaurants.CountAsync();
    }
}
