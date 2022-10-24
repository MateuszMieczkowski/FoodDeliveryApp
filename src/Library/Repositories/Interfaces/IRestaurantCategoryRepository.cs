using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IRestaurantCategoryRepository
{
    public IEnumerable<RestaurantCategory> Categories { get; set; }
    public Task AddCategoryAsync(RestaurantCategory category);
    public Task DeleteCategoryAsync(RestaurantCategory category);
    public Task<RestaurantCategory?> GetRestaurantCategory(string name);
}
