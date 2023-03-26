using Library.Entities;

namespace Library.Services.DiscountCalculator;

public interface IDiscountCalculator
{
    decimal CalculateDiscountAmount(Order order);
    bool SupportsOrder(Order order);
}