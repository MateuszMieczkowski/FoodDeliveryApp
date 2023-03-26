using Library.Entities;

namespace Library.Services.DiscountCalculator;

public class DiscountCalculatorFactory : IDiscountCalculatorFactory
{
    private readonly IEnumerable<IDiscountCalculator> _discountCalculators;

    public DiscountCalculatorFactory(IEnumerable<IDiscountCalculator> discountCalculators)
    {
        _discountCalculators = discountCalculators;
    }
    public IDiscountCalculator GetCalculator(Order order)
    {
        if (order?.Discount is null)
        {
            throw new NullReferenceException("Order and Discount can't be null.");
        }
        return _discountCalculators.Single(x => x.SupportsOrder(order));
    }
}