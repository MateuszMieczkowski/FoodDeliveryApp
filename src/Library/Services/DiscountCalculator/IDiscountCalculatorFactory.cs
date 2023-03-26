using Library.Entities;

namespace Library.Services.DiscountCalculator;

public interface IDiscountCalculatorFactory
{
    IDiscountCalculator GetCalculator(Order order);
}