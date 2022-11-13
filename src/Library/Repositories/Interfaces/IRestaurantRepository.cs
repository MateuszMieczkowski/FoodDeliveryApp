using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize);
    Task AddRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurantAsync(Restaurant restaurant);
    Task<Restaurant?> GetRestaurantAsync(int id);
    Task<int> SaveChangesAsync();
}
