using AutoMapper;
using Library.Authorization;
using Library.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Models.OrderDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Library.Services.OrderCostCalculator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Library.Enums.Enums;

namespace Library.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRequirementService _requirementService;
    private readonly IOrderCostCalculator _orderCostCalculator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private const int MaxPageSize = 50;

	public OrderService(IOrderRepository orderRepository, IShoppingCartService shoppingCartService,
		UserManager<User> userManager, IRestaurantRepository restaurantRepository, IMapper mapper,
        IRequirementService requirementService, IOrderCostCalculator orderCostCalculator)
	{
		_orderRepository = orderRepository;
		_shoppingCartService = shoppingCartService;
		_userManager = userManager;
		_restaurantRepository = restaurantRepository;
		_mapper = mapper;
		_requirementService = requirementService;
		_orderCostCalculator = orderCostCalculator;
	}

	public async Task CreateOrderAsync(Guid userId, int restaurantId, Guid shoppingCartId, AddressDto? addressDto)
    {
        await AuthorizeUserAndRestaurantManager(userId, restaurantId);

        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException($"There's no such user with id: {userId}");
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
            ?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}");
        var shoppingCart = await _shoppingCartService.GetShoppingCartAsync(restaurantId, shoppingCartId);
        if (!shoppingCart.ShoppingCartItems.Any())
        {
            throw new BadRequestException("Empty cart or wrong restaurantId");
        }

        var orderItems = shoppingCart.ShoppingCartItems
			.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                ProductQuantity = x.Quantity
            }).ToList();

        var order = new Order
        {
            RestaurantId = restaurantId,
            OrderItems = orderItems,
            UserId = userId,
            Status = OrderStatus.InPreparation,
        };
        _orderCostCalculator.CalculateCost(order);
        if (addressDto is not null)
        {
            order.Address = _mapper.Map<Address>(addressDto);
        }

        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task DeleteOrder(Guid orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId)
            ?? throw new NotFoundException($"Order with id: {orderId} not found");
        await AuthorizeRestaurantManager(order.RestaurantId);

        _orderRepository.DeleteOrder(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task<OrderDto> GetOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId)
            ?? throw new NotFoundException($"Order with id: {orderId} not found");
        await AuthorizeUserAndRestaurantManager(order.UserId, order.RestaurantId);

        var dto = _mapper.Map<OrderDto>(order);
        return dto;
    }

    public async Task<PagedResult<OrderDto>> GetOrdersAsync(Guid userId, int pageNumber, int pageSize)
    {
        await AuthorizeUser(userId);
        Expression<Func<Order, bool>> filter = x => x.UserId == userId;
        return await Paginate(filter, pageNumber, pageSize);
    }

    public async Task<PagedResult<OrderDto>> GetOrdersAsync(int restaurantId, int pageNumber, int pageSize)
    {
        await AuthorizeRestaurantManager(restaurantId);
        Expression<Func<Order, bool>> filter = x => x.RestaurantId == restaurantId;
        return await Paginate(filter, pageNumber, pageSize);
    }

    private async Task<PagedResult<OrderDto>> Paginate(Expression<Func<Order, bool>> filter, int pageNumber, int pageSize)
    {
        if (pageSize > MaxPageSize || pageSize <= 0 || pageNumber <= 0)
        {
            throw new BadRequestException("Wrong page size or page number.");
        }
        var baseQuery = _orderRepository.Orders.Where(filter);

        var orders = await baseQuery.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

        var totalResultsCount = await baseQuery.CountAsync();

        if ((pageNumber - 1) * pageSize > totalResultsCount)
        {
            throw new BadRequestException("Wrong page size or page number.");
        }

        var dtos = _mapper.Map<List<OrderDto>>(orders);
        return new PagedResult<OrderDto>(dtos, pageNumber, pageSize, totalResultsCount);
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus? status)
    {
        var order = await _orderRepository.GetOrderAsync(orderId)
            ?? throw new NotFoundException($"There's no order with id: {orderId}.");
        if (status is null)
        {
            throw new BadRequestException("Status cannot be null.");
        }
        await AuthorizeRestaurantManager(order.RestaurantId);
        order.Status = status.Value;
        await _orderRepository.SaveChangesAsync();
    }

    private async Task AuthorizeUser(Guid userId)
    {
        var result = await _requirementService.AuthorizeAsync(new AccountOwnerRequirement(userId));
        if (!result.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

    private async Task AuthorizeRestaurantManager(int restaurantId)
    {
        var result = await _requirementService.AuthorizeAsync(new RestaurantManagerRequirement(restaurantId));
        if (!result.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

    private async Task AuthorizeUserAndRestaurantManager(Guid userId, int restaurantId)
    {
        var userResult = await _requirementService.AuthorizeAsync(new AccountOwnerRequirement(userId));
        var managerResult = await _requirementService.AuthorizeAsync(new RestaurantManagerRequirement(restaurantId));
        if (!userResult.Succeeded && !managerResult.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

}
