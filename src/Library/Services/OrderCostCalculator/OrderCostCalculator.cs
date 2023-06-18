using Library.Entities;
using Library.Services.DiscountCalculator;

namespace Library.Services.OrderCostCalculator;

public class OrderCostCalculator : IOrderCostCalculator
{
    private readonly IDiscountCalculatorFactory _discountCalculatorFactory;

    public OrderCostCalculator(IDiscountCalculatorFactory discountCalculatorFactory)
    {
        _discountCalculatorFactory = discountCalculatorFactory;
    }

    public void CalculateCost(Order order)
    {
        order.DiscountAmount = CalculateDiscountAmount(order);
        order.Total = order.OrderItems.Sum(x => x.ProductQuantity * x.Product.Price);
        order.DiscountedTotal = order.Total - order.DiscountAmount;
    }

    private decimal CalculateDiscountAmount(Order order)
    {
        if (order.Discount is null)
        {
            return 0m;
        }

        var discountCalculator = _discountCalculatorFactory.GetCalculator(order);
        var discountAmount = discountCalculator.CalculateDiscountAmount(order);
        return discountAmount;
    }
}