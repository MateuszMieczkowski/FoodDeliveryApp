using Library.Models.ProductDtos;

namespace Library.Models.ShoppingCartDtos;

public class ShoppingCartItemDto
{
    public int Id { get; set; }

    public Guid ShoppingCartId { get; set; }

    public int ProductId { get; set; }

    public ProductDto Product { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal Total { get; set; }
}