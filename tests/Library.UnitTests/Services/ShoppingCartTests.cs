using System;
using System.Threading.Tasks;
using AutoMapper;
using Library.Entities;
using Library.Exceptions;
using Library.Models.ShoppingCartDtos;
using Library.Repositories.Interfaces;
using Library.Services.ShoppingCart;
using Moq;
using Xunit;

namespace Library.Tests.Services.ShoppingCart;

public class ShoppingCartServiceTests
{
	private readonly Mock<IShoppingCartItemRepository> _mockShoppingCartItemRepository;
	private readonly Mock<IProductRepository> _mockProductRepository;
	private readonly Mock<IMapper> _mockMapper;
	private readonly ShoppingCartService _service;

	public ShoppingCartServiceTests()
	{
		_mockShoppingCartItemRepository = new Mock<IShoppingCartItemRepository>();
		_mockProductRepository = new Mock<IProductRepository>();
		_mockMapper = new Mock<IMapper>();
		_service = new ShoppingCartService(_mockShoppingCartItemRepository.Object, _mockProductRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task AddToCartAsync_ProductExists_ShoppingCartItemAdded()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		var product = new Product();
		var shoppingCartItem = new ShoppingCartItem();
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync(product);
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemAsync(shoppingCartId, productId)).ReturnsAsync((ShoppingCartItem?)null);
		_mockMapper.Setup(m => m.Map<ShoppingCartItem>(It.IsAny<ShoppingCartItemDto>())).Returns(shoppingCartItem);

		// Act
		await _service.AddToCartAsync(productId, shoppingCartId);

		// Assert
		_mockProductRepository.Verify(r => r.GetProductAsync(productId), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemAsync(shoppingCartId, productId), Times.Once);
		_mockMapper.Verify(m => m.Map<ShoppingCartItem>(It.IsAny<ShoppingCartItemDto>()), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.AddShoppingCartItemAsync(shoppingCartItem), Times.Once);
	}

	[Fact]
	public async Task AddToCartAsync_ProductDoesNotExist_ThrowsNotFoundException()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync((Product?)null);

		// Act & Assert
		await Assert.ThrowsAsync<NotFoundException>(() => _service.AddToCartAsync(productId, shoppingCartId));
	}

	[Fact]
	public async Task AddToCartAsync_ShoppingCartItemExists_DoesNotAddShoppingCartItem()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		var shoppingCartItem = new ShoppingCartItem();
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync(new Product());
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemAsync(shoppingCartId, productId)).ReturnsAsync(shoppingCartItem);

		// Act
		await _service.AddToCartAsync(productId, shoppingCartId);

		// Assert
		_mockProductRepository.Verify(r => r.GetProductAsync(productId), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemAsync(shoppingCartId, productId), Times.Once);
		_mockMapper.Verify(m => m.Map<ShoppingCartItem>(It.IsAny<ShoppingCartItemDto>()), Times.Never);
		_mockShoppingCartItemRepository.Verify(r => r.AddShoppingCartItemAsync(It.IsAny<ShoppingCartItem>()), Times.Never);
	}
	[Fact]
	public async Task DeleteFromCartAsync_ProductExists_ShoppingCartItemDeleted()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		var product = new Product { Price = 10 };
		var shoppingCartItem = new ShoppingCartItem { Quantity = 1, Total = 10 };
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync(product);
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemAsync(shoppingCartId, productId)).ReturnsAsync(shoppingCartItem);

		// Act
		await _service.DeleteFromCartAsync(productId, shoppingCartId);

		// Assert
		_mockProductRepository.Verify(r => r.GetProductAsync(productId), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemAsync(shoppingCartId, productId), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.DeleteShoppingCartItem(shoppingCartItem), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
	}

	[Fact]
	public async Task DeleteFromCartAsync_ProductDoesNotExist_ThrowsNotFoundException()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync((Product?)null);

		// Act & Assert
		await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteFromCartAsync(productId, shoppingCartId));
	}

	[Fact]
	public async Task DeleteFromCartAsync_ShoppingCartItemDoesNotExist_ThrowsNotFoundException()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync(new Product());
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemAsync(shoppingCartId, productId)).ReturnsAsync((ShoppingCartItem?)null);

		// Act & Assert
		await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteFromCartAsync(productId, shoppingCartId));
	}

	[Fact]
	public async Task DeleteFromCartAsync_ShoppingCartItemQuantityGreaterThanOne_ShoppingCartItemUpdated()
	{
		// Arrange
		var productId = 1;
		var shoppingCartId = Guid.NewGuid();
		var product = new Product { Price = 10 };
		var shoppingCartItem = new ShoppingCartItem { Quantity = 2, Total = 20 };
		_mockProductRepository.Setup(r => r.GetProductAsync(productId)).ReturnsAsync(product);
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemAsync(shoppingCartId, productId)).ReturnsAsync(shoppingCartItem);

		// Act
		await _service.DeleteFromCartAsync(productId, shoppingCartId);

		// Assert
		_mockProductRepository.Verify(r => r.GetProductAsync(productId), Times.Once);
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemAsync(shoppingCartId, productId), Times.Once);
		Assert.Equal(1, shoppingCartItem.Quantity);
		Assert.Equal(10, shoppingCartItem.Total);
		_mockShoppingCartItemRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
	}

	[Fact]
	public async Task GetShoppingCartAsync_ShoppingCartExists_ReturnsShoppingCartDto()
	{
		// Arrange
		var restaurantId = 1;
		var shoppingCartId = Guid.NewGuid();
		var shoppingCartItems = new List<ShoppingCartItem>
		{
			new ShoppingCartItem { ProductId = 1, Quantity = 2, Total = 20 },
			new ShoppingCartItem { ProductId = 2, Quantity = 1, Total = 15 }
		};
		var shoppingCart = new Library.Services.ShoppingCart.ShoppingCart { ShoppingCartId = shoppingCartId, ShoppingCartItems = shoppingCartItems };
		var shoppingCartDto = new ShoppingCartDto { ShoppingCartId = shoppingCartId, ShoppingCartItems = shoppingCartItems
			.Select(i => new ShoppingCartItemDto { ProductId = i.ProductId, Quantity = i.Quantity, Total = i.Total }).ToList() };
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemsAsync(shoppingCartId, restaurantId)).ReturnsAsync(shoppingCartItems);
		_mockMapper.Setup(m => m.Map<ShoppingCartDto>(shoppingCart)).Returns(shoppingCartDto);

		// Act
		var result = await _service.GetShoppingCartAsync(restaurantId, shoppingCartId);

		// Assert
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemsAsync(shoppingCartId, restaurantId), Times.Once);
		_mockMapper.Verify(m => m.Map<ShoppingCartDto>(shoppingCart), Times.Once);
		Assert.Equal(shoppingCartDto, result);
	}

	[Fact]
	public async Task GetShoppingCartAsync_ShoppingCartDoesNotExist_ReturnsNull()
	{
		// Arrange
		var restaurantId = 1;
		var shoppingCartId = Guid.NewGuid();
		_mockShoppingCartItemRepository.Setup(r => r.GetShoppingCartItemsAsync(shoppingCartId, restaurantId)).ReturnsAsync((List<ShoppingCartItem>?)null);

		// Act
		var result = await _service.GetShoppingCartAsync(restaurantId, shoppingCartId);

		// Assert
		_mockShoppingCartItemRepository.Verify(r => r.GetShoppingCartItemsAsync(shoppingCartId, restaurantId), Times.Once);
		Assert.Null(result);
	}
}