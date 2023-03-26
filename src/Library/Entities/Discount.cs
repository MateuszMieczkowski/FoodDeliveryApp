namespace Library.Entities;

public abstract class Discount
{
    public int Id { get; set; }

    public string DiscountCode { get; set; } = string.Empty;
    
    public ICollection<Order>? Orders { get; set; }
}