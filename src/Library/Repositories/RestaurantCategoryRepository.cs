using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class RestaurantCategoryRepository : IRestaurantCategoryRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public RestaurantCategoryRepository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        Categories = dbcontext.RestaurantCategories.Include(r => r.Restaurants);
    }

    public IEnumerable<RestaurantCategory> Categories { get; set; }

    public async Task AddCategoryAsync(RestaurantCategory category)
    {
        if(_dbcontext.RestaurantCategories.Any(r=>r.Name == category.Name))
        {
            return;
        }
        await _dbcontext.RestaurantCategories.AddAsync(category);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(RestaurantCategory category)
    {
        _dbcontext.RestaurantCategories.Remove(category);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<RestaurantCategory?> GetRestaurantCategory(string name)
    {
        return await _dbcontext.RestaurantCategories.SingleOrDefaultAsync(r => r.Name == name);
    }
}
