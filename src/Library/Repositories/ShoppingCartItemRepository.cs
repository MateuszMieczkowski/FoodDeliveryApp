using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class ShoppingCartItemRepository : IShoppingCartItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ShoppingCartItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddShoppingCartItemAsync(ShoppingCartItem shoppingCartItem)
    {
        await _dbContext.ShoppingCartItems.AddAsync(shoppingCartItem);
    }

    public void DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem)
    {
        _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
    }

    public async Task<ShoppingCartItem?> GetShoppingCartItemAsync(Guid shoppingCartId, int productId)
    {
        var item =  await _dbContext.ShoppingCartItems
            .FirstOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        return item;
    }

    public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(Guid shoppingCartId, int restaurantId)
    {
        var shoppingCartItems = await _dbContext.ShoppingCartItems
            .Include(r => r.Product)
            .Where(r => r.ShoppingCartId == shoppingCartId && r.Product.RestaurantId == restaurantId)
            .ToListAsync();

        return shoppingCartItems;
    }

    public async Task<int> SaveChangesAsync()
    {
       return await _dbContext.SaveChangesAsync();
    }
}
