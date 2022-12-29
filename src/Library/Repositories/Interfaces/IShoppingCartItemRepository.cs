using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IShoppingCartItemRepository
{
    Task AddShoppingCartItemAsync(ShoppingCartItem shoppingCartItem);

    void DeleteShoppingCartItem (ShoppingCartItem shoppingCartItem);

    List<ShoppingCartItem> GetShoppingCartItems(Guid shoppingCartId);

    Task<int> SaveChangesAsync();
}
