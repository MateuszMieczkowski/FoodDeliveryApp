using AutoMapper;
using Library.Authorization;
using Library.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Models.OrderDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
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
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private const int MaxPageSize = 50;

    public OrderService(IOrderRepository orderRepository, IShoppingCartService shoppingCartService,
        UserManager<User> userManager, IRestaurantRepository restaurantRepository, IMapper mapper, IAuthorizationService authorizationService, IUserContextAccessor userContextAccessor)
    {
        _orderRepository = orderRepository;
        _shoppingCartService = shoppingCartService;
        _userManager = userManager;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _userContextAccessor = userContextAccessor;
    }

    public async Task CreateOrderAsync(Guid? userId, int? restaurantId, AddressDto? addressDto)
    {
        if(userId is null || restaurantId is null)
        {
            throw new BadRequestException("userId and restaurantId are required");
        }
        await AuthorizeUserAndRestaurantManager(userId.Value, restaurantId.Value);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if(user is null)
        {
            throw new NotFoundException($"There's no such user with id: {userId}");
        }
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId.Value);
        if (restaurant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId.Value}");
        }

        var cartItems = _shoppingCartService.GetShoppingCart().ShoppingCartItems?.Where(x => x.Product.RestaurantId == restaurantId.Value);
        if(cartItems is null || !cartItems.Any())
        {
            throw new BadRequestException("Empty cart or wrong restaurantId");
        }

        var orderItems = cartItems.Select(x => new OrderItem
        {
            ProductId = x.ProductId,
            ProductQuantity = x.Quantity
        }).ToList();

        var order = new Order
        {
            RestaurantId = restaurantId.Value,
            OrderItems = orderItems,
            UserId = userId.Value,
            Status = OrderStatus.InPreparation,
            Total = cartItems.Sum(x=>x.Total),
        };
        if(addressDto is not null)
        {
            order.Address = _mapper.Map<Address>(addressDto);
        }

        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task DeleteOrder(Guid orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order is null)
        {
            throw new NotFoundException($"Order with id: {orderId} not found");
        }

        await AuthorizeRestaurantManager(order.RestaurantId);

        _orderRepository.DeleteOrder(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task<OrderDto> GetOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order is null)
        {
            throw new NotFoundException($"Order with id: {orderId} not found");
        }
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
        //var baseQuery = orders;

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
        var order = await _orderRepository.GetOrderAsync(orderId);
        if(order is null)
        {
            throw new NotFoundException($"There's no order with id: {orderId}.");
        }
        if(status is null)
        {
            throw new BadRequestException("Status cannot be null.");
        }
        await AuthorizeRestaurantManager(order.RestaurantId);
        order.Status = status.Value;
        await _orderRepository.SaveChangesAsync();
    }

    private async Task<AuthorizationResult> Authorize(IAuthorizationRequirement requirement)
    {
        var user = _userContextAccessor.User;
        if(user is null)
        {
            throw new Exception();
        }

        var authResult = await _authorizationService.AuthorizeAsync(user, null, requirement);
        return authResult;
    }

    private async Task AuthorizeUser(Guid userId)
    {
        var result = await Authorize(new AccountOwnerRequirement(userId));
        if(!result.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

    private async Task AuthorizeRestaurantManager(int restaurantId)
    {
        var result = await Authorize(new RestaurantManagerRequirement(restaurantId));
        if (!result.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

    private async Task AuthorizeUserAndRestaurantManager(Guid userId, int restaurantId)
    {
        var userResult = await Authorize(new AccountOwnerRequirement(userId));
        var managerResult = await Authorize(new RestaurantManagerRequirement(restaurantId));
        if (!userResult.Succeeded && !managerResult.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

}
