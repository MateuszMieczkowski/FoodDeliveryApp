using Library.Entities;

namespace Library.Services.ShoppingCart;

public class ShoppingCart
{
    public Guid ShoppingCartId { get; set; }

    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
}
