using AutoMapper;
using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Exceptions;
using Web.Api.Models.ShoppingCartDtos;

namespace Library.Services.ShoppingCart;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartService(IServiceProvider serviceProvider, IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _shoppingCartItemRepository = shoppingCartItemRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _shoppingCart = InitializeShoppingCart();
    }

    private ShoppingCart InitializeShoppingCart()
    {
        var httpContext = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

        string? shoppingCartIdFromSession = httpContext?.Session.GetString("shoppingCartId");
        Guid shoppingCartId;
        if (!Guid.TryParse(shoppingCartIdFromSession, out shoppingCartId))
        {
            shoppingCartId = Guid.NewGuid();
        }

        httpContext?.Session.SetString("shoppingCartId", shoppingCartId.ToString());

        var shoppingCartItems = _shoppingCartItemRepository.GetShoppingCartItems(shoppingCartId);

        return new ShoppingCart() { ShoppingCartId = shoppingCartId, ShoppingCartItems = shoppingCartItems };

    }

    public async Task AddToCartAsync(int productId)
    {
        var shoppingCartItem = _shoppingCart!.ShoppingCartItems!.SingleOrDefault(r => r.ProductId == productId);

        if (shoppingCartItem is null)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product is null)
            {
                throw new NotFoundException($"Product with id {productId} does not exists");
            }

            var newItem = new ShoppingCartItem()
            {
                Product = product,
                ShoppingCartId = _shoppingCart.ShoppingCartId,
                Quantity = 1
            };

            await _shoppingCartItemRepository.AddShoppingCartItemAsync(newItem);
            await _productRepository.SaveChangesAsync();
            return;
        }

        shoppingCartItem.Quantity++;
        await _shoppingCartItemRepository.SaveChangesAsync();
    }
    public async Task DeleteFromCartAsync(int productId)
    {
        var shoppingCartItem = _shoppingCart!.ShoppingCartItems!.SingleOrDefault(r => r.Id == productId);
        if (shoppingCartItem is null)
        {
            throw new NotFoundException($"There's no such product with id: {productId} in shopping cart");
        }

        if (shoppingCartItem.Quantity == 1)
        {
            _shoppingCartItemRepository.DeleteShoppingCartItem(shoppingCartItem);
            await _shoppingCartItemRepository.SaveChangesAsync();
            return;
        }
        shoppingCartItem.Quantity--;
        await _shoppingCartItemRepository.SaveChangesAsync();
    }

    public ShoppingCartDto GetShoppingCart()
    {
        var dto = _mapper.Map<ShoppingCartDto>(_shoppingCart);
        return dto;
    }
}
