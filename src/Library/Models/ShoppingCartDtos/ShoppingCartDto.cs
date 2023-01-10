namespace Library.Models.ShoppingCartDtos;

public class ShoppingCartDto
{
    public Guid ShoppingCartId { get; set; }

    public ICollection<ShoppingCartItemDto>? ShoppingCartItems { get; set; }
}