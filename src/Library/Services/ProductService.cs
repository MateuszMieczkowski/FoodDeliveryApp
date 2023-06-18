using AutoMapper;
using Library.Authorization;
using Library.Entities;
using Library.Exceptions;
using Library.Models.ProductDtos;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Library.Services;

public class ProductService : IProductService
{
	private readonly IRestaurantRepository _restaurantRepository;
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly IRequirementService _requirementService;

	public ProductService(IRestaurantRepository restaurantRepository, IProductRepository productRepository, IMapper mapper, IRequirementService requirementService)
	{
		_restaurantRepository = restaurantRepository;
		_productRepository = productRepository;
		_mapper = mapper;
		_requirementService = requirementService;
	}

	public async Task CreateProductAsync(int restaurantId, ProductForUpdateDto dto)
	{
		var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
		await AuthorizeManager(restaurant);

		var productCategory = _productRepository.GetCategories().SingleOrDefault(r => r.Id == dto.ProductCategoryId)
			?? throw new BadRequestException($"There's not such productCategory with id: {dto.ProductCategoryId}");
		var newProduct = _mapper.Map<Product>(dto);

		await _productRepository.AddProductAsync(newProduct);
		await _productRepository.SaveChangesAsync();
	}

	public async Task DeleteProductAsync(int productId)
	{
		var product = await _productRepository.GetProductAsync(productId)
			?? throw new NotFoundException($"There's no such product with id: {productId}.");

		var restaurant = await _restaurantRepository.GetRestaurantAsync(product.RestaurantId);
		await AuthorizeManager(restaurant!);

		_productRepository.DeleteProduct(product);
		await _productRepository.SaveChangesAsync();
	}

	public async Task<List<ProductDto>> GetProductsAsync(int restaurantId)
	{
		_ = await _restaurantRepository.GetRestaurantAsync(restaurantId)
			?? throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
		var products = await _productRepository.GetRestaurantProductsAsync(restaurantId);
		var productDtos = _mapper.Map<List<ProductDto>>(products);
		return productDtos;
	}

	private async Task AuthorizeManager(Restaurant restaurant)
	{

		var authorizationResult =
			await _requirementService.AuthorizeAsync(new RestaurantManagerRequirement(restaurant.Id));

		if (!authorizationResult.Succeeded)
		{
			throw new ForbiddenException();
		}
	}
}
