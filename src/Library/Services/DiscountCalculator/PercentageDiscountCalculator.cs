using Library.Entities;

namespace Library.Services.DiscountCalculator;

public class PercentageDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscountAmount(Order order)
    {
        var discount = order.Discount as PercentageDiscount;
        return order.Total * discount!.Percentage;
    }

    public bool SupportsOrder(Order order)
    {
        return order.Discount is PercentageDiscount;
    }
}