using Library.Entities;

namespace Library.Services.ShoppingCart;

public class ShoppingCart
{
    public Guid ShoppingCartId { get; set; }
    
    public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
}
