using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantCategoryRepository : IRestaurantCategoryRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public IEnumerable<RestaurantCategory>? Categories { get; set; }

    public RestaurantCategoryRepository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        Categories = dbcontext.RestaurantCategories;
    }

    public async Task AddCategoryAsync(RestaurantCategory category)
    {
        await _dbcontext.RestaurantCategories.AddAsync(category);
    }

    public void DeleteCategory(RestaurantCategory category)
    {
        _dbcontext.RestaurantCategories.Remove(category);
    }

    public async Task<RestaurantCategory?> GetRestaurantCategory(string name)
    {
        return await _dbcontext.RestaurantCategories.SingleOrDefaultAsync(r => r.Name == name);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<RestaurantCategory>> GetAllCategoriesAsync()
    {
        var categories = await _dbcontext.RestaurantCategories.ToListAsync();
        return categories;
    }
}
