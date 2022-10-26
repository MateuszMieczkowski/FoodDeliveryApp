using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantCategoryRepository
{
    IEnumerable<RestaurantCategory> Categories { get; set; }
    Task AddCategoryAsync(RestaurantCategory category);
    Task DeleteCategoryAsync(RestaurantCategory category);
    Task<RestaurantCategory?> GetRestaurantCategory(string name);
    Task<int> SaveChangesAsync();

}
