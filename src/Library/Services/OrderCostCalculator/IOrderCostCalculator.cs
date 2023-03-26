using Library.Entities;

namespace Library.Services.OrderCostCalculator;

public interface IOrderCostCalculator
{
    void CalculateCost(Order order);
}