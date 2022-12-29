using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantRepository
{
    IQueryable<Restaurant> Restaurants { get; set; }

    Task<int> GetRestaurantsCount();

    Task<List<Restaurant>> GetAllRestaurantsAsync();

    Task AddRestaurantAsync(Restaurant restaurant);

    Task DeleteRestaurantAsync(Restaurant restaurant);

    Task<Restaurant?> GetRestaurantAsync(int restaurantId);

    Task<int> SaveChangesAsync();
}
