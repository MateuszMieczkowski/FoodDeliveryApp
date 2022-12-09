using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantCategoryRepository
{
    IEnumerable<RestaurantCategory> Categories { get; set; }

    Task AddCategoryAsync(RestaurantCategory category);

    void DeleteCategory(RestaurantCategory category);

    Task<RestaurantCategory?> GetRestaurantCategory(string categoryName);

    Task<List<RestaurantCategory>> GetAllCategoriesAsync();

    Task<int> SaveChangesAsync();

}
