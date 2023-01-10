namespace Library.Entities;

public class Product
{
    public int Id { get; set; }
   
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool InStock { get; set; }

    public ProductCategory Category { get; set; } = default!;

    public int ProductCategoryId { get; set; }

    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }

    public ICollection<ShoppingCartItem>? ShoppingCartItems{ get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}
