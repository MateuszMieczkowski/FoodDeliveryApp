namespace UI.API.Responses;

public class ShoppingCartItem
{
	public int Id { get; set; }

	public Guid ShoppingCartId { get; set; }

	public int ProductId { get; set; }

	public Product Product { get; set; } = default!;

	public int Quantity { get; set; }

	public decimal Total { get; set; }
}
