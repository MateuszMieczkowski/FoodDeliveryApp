using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantCategoryRepository : IRestaurantCategoryRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public IEnumerable<RestaurantCategory> Categories { get; set; }

    public RestaurantCategoryRepository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        Categories = dbcontext.RestaurantCategories.Include(r => r.Restaurants);
    }

    public async Task<bool> AddCategoryAsync(RestaurantCategory category)
    {
        if(_dbcontext.RestaurantCategories.Any(r=>r.Name == category.Name))
        {
            return false;
        }
        await _dbcontext.RestaurantCategories.AddAsync(category);
        return true;
    }

    public async Task<bool> DeleteCategory(string categoryName)
    {
        var category = await _dbcontext.RestaurantCategories.FindAsync(categoryName);
        if(category is null)
        {
            return false;
        }
        _dbcontext.RestaurantCategories.Remove(category);
        return true;
    }

    public async Task<RestaurantCategory?> GetRestaurantCategory(string name)
    {
        return await _dbcontext.RestaurantCategories.Include(r=>r.Restaurants).SingleOrDefaultAsync(r => r.Name == name);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<RestaurantCategory>> GetAllCategoriesAsync()
    {
        var categories = await _dbcontext.RestaurantCategories.Include(r => r.Restaurants).ToListAsync();
        return categories;
    }
}
