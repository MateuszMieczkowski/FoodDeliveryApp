using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantCategoryRepository
{
    IEnumerable<RestaurantCategory> Categories { get; set; }
    Task AddCategoryAsync(RestaurantCategory category);
    void DeleteCategory(RestaurantCategory category);
    Task<RestaurantCategory?> GetRestaurantCategory(string name);
    Task<int> SaveChangesAsync();

}
