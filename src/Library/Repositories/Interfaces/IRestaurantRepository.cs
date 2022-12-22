using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantRepository
{
    public IEnumerable<Restaurant>? Restaurants { get; set; }

    Task<List<Restaurant>> GetAllRestaurantsAsync();

    Task<List<Restaurant>> GetRestaurantsAsync(string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize);

    Task AddRestaurantAsync(Restaurant restaurant);

    Task DeleteRestaurantAsync(Restaurant restaurant);

    Task<Restaurant?> GetRestaurantAsync(int restaurantId);

    Task<int> SaveChangesAsync();
}
