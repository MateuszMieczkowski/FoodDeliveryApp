using AutoMapper;
using Library.Entities;
using Library.Exceptions;
using Library.Models.ShoppingCartDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;

namespace Library.Services.ShoppingCart;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ShoppingCartService(IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository, IMapper mapper)
    {
        _shoppingCartItemRepository = shoppingCartItemRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task AddToCartAsync(int productId, Guid shoppingCartId)
    {
        var product = await _productRepository.GetProductAsync(productId)
            ?? throw new NotFoundException($"Product with id {productId} does not exists");

        if(!product.InStock)
        {
            throw new BadRequestException("Product is out of stock.");
        }
        var shoppingCartItem = await _shoppingCartItemRepository.GetShoppingCartItemAsync(shoppingCartId, productId);

        if (shoppingCartItem is null)
        {
            var newItem = new ShoppingCartItem()
            {
                Product = product,
                ShoppingCartId = shoppingCartId,
                Quantity = 1,
                Total = product.Price
            };

            await _shoppingCartItemRepository.AddShoppingCartItemAsync(newItem);
            await _productRepository.SaveChangesAsync();
            return;
        }

        shoppingCartItem.Total += product.Price;
        shoppingCartItem.Quantity++;
        await _shoppingCartItemRepository.SaveChangesAsync();
    }
    public async Task DeleteFromCartAsync(int productId, Guid shoppingCartId)
    {
        var product = await _productRepository.GetProductAsync(productId)
            ?? throw new NotFoundException($"Product with id {productId} does not exists");
        var shoppingCartItem = await _shoppingCartItemRepository.GetShoppingCartItemAsync(shoppingCartId, productId)
            ?? throw new NotFoundException($"There's no such product with id: {productId} in shopping cart");
        if (shoppingCartItem.Quantity == 1)
        {
            _shoppingCartItemRepository.DeleteShoppingCartItem(shoppingCartItem);
            await _shoppingCartItemRepository.SaveChangesAsync();
            return;
        }

        shoppingCartItem.Total -= product.Price;
        shoppingCartItem.Quantity--;
        await _shoppingCartItemRepository.SaveChangesAsync();
    }

    public async Task<ShoppingCartDto> GetShoppingCartAsync(int restaurantId, Guid shoppingCartId)
    {
        var items = await _shoppingCartItemRepository
            .GetShoppingCartItemsAsync(shoppingCartId, restaurantId);
        var shoppingCart = new ShoppingCart
        {
            ShoppingCartId = shoppingCartId,
            ShoppingCartItems = items
        };

        return _mapper.Map<ShoppingCartDto>(shoppingCart);
    }
}
