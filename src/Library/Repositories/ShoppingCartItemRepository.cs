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

    public List<ShoppingCartItem> GetShoppingCartItems(Guid shoppingCartId)
    {
        var shoppingCartItems = _dbContext.ShoppingCartItems.Include(r => r.Product)
                                                            .Where(r => r.ShoppingCartId.Equals(shoppingCartId))
                                                            .ToList();
        return shoppingCartItems;
    }

    public async Task<int> SaveChangesAsync()
    {
       return await _dbContext.SaveChangesAsync();
    }
}
