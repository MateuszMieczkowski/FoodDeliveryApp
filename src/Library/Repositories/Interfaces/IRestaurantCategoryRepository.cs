using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantCategoryRepository
{
    IEnumerable<RestaurantCategory> Categories { get; set; }

    Task<bool> AddCategoryAsync(RestaurantCategory category);

    Task<bool> DeleteCategory(string categoryName);

    Task<RestaurantCategory?> GetRestaurantCategory(string categoryName);

    Task<List<RestaurantCategory>> GetAllCategoriesAsync();

    Task<int> SaveChangesAsync();

}
