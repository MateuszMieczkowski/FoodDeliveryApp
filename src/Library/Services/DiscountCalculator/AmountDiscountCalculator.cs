using Library.Entities;

namespace Library.Services.DiscountCalculator;

public class AmountDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscountAmount(Order order)
    {
        var discount = order.Discount as AmountDiscount;
        return discount!.Amount;
    }

    public bool SupportsOrder(Order order)
    {
        return order.Discount is AmountDiscount;
    }
}