using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Orders = dbContext.Orders;
    }

    public IQueryable<Order> Orders { get; set; }

    public async Task AddOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public void DeleteOrder(Order order)
    {
        _dbContext.Orders.Remove(order);
    }

    public async Task<List<Order>> GetAllOrders(Order order)
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderAsync(Guid orderId)
    {
        return await _dbContext.Orders.FindAsync(orderId);
    }

    public async Task<List<Order>> GetRestaurantOrders(int restaurantId)
    {
        return await _dbContext.Orders.Where(x => x.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<List<Order>> GetUserOrders(Guid userId)
    {
        return await _dbContext.Orders.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
