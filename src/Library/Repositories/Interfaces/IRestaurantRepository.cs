using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantRepository
{
    IEnumerable<Restaurant> AllRestaurants { get; set; }
    Task AddRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurantAsync(Restaurant restaurant);
    Task<Restaurant?> GetRestaurantAsync(int id);
    Task<int> SaveChangesAsync();
}
