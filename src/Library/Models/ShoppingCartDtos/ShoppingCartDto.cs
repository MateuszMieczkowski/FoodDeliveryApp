namespace Library.Models.ShoppingCartDtos;

public class ShoppingCartDto
{
    public Guid ShoppingCartId { get; set; }

    public IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; } = Enumerable.Empty<ShoppingCartItemDto>();
}