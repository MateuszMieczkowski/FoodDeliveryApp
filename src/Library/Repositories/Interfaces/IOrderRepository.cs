using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IOrderRepository
{
    IQueryable<Order> Orders { get; set; }

    Task<List<Order>> GetAllOrders(Order order);

    Task<List<Order>> GetUserOrders(Guid userId);

    Task<List<Order>> GetRestaurantOrders(int restaurantId);

    Task AddOrderAsync(Order order);

    void DeleteOrder(Order order);

    Task<Order?> GetOrderAsync(Guid orderId);

    Task<int> SaveChangesAsync();
}
