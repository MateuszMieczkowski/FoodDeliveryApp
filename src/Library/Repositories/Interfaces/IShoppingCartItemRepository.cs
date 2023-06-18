using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IShoppingCartItemRepository
{
    Task AddShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);

    void DeleteShoppingCartItem (ShoppingCartItem shoppingCartItem);

    Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(Guid shoppingCartId, int restaurantId);

    Task<ShoppingCartItem?> GetShoppingCartItemAsync(Guid shoppingCartId, int productId);

    Task<int> SaveChangesAsync();
}
