using API.Authorization;
using API.Exceptions;
using API.Models.ProductDtos;
using API.Services.Interfaces;
using AutoMapper;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Services;

public class ProductService : IProductService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextAccessor _userContextAccessor;

    public ProductService(IRestaurantRepository restaurantRepository, IProductRepository productRepository, IMapper mapper, IAuthorizationService authorizationService, IUserContextAccessor userContextAccessor)
    {
        _restaurantRepository = restaurantRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _userContextAccessor = userContextAccessor;
    }

    public async Task CreateProductAsync(int restaurantId, ProductForUpdateDto dto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
        }

        await AuthorizeManager(restaurant);

        var productCategory = _productRepository.GetCategories().SingleOrDefault(r => r.Id == dto.ProductCategoryId);
        if (productCategory is null)
        {
            throw new BadRequestException($"There's not such productCategory with id: {dto.ProductCategoryId}");
        }

        var newProduct = _mapper.Map<Product>(dto);

        await _productRepository.AddProductAsync(newProduct);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int restaurantId, int productId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
        }

        await AuthorizeManager(restaurant);

        var product = await _productRepository.GetProductAsync(productId);
        if (product is null)
        {
            throw new NotFoundException($"There's no such product with id: {productId}.");
        }

        if (product.RestaurantId != restaurantId)
        {
            throw new BadRequestException("Wrong restaurant id or product id.");
        }

        _productRepository.DeleteProduct(product);
        await _productRepository.SaveChangesAsync();
    }

    private async Task AuthorizeManager(Restaurant restaurant)
    {
        var user = _userContextAccessor.User;
        if (user is null)
        {
            throw new Exception();
        }

        var authorizationResult = await
            _authorizationService.AuthorizeAsync(user, restaurant, new RestaurantManagerRequirement());

        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException();
        }
    }

    public async Task<List<ProductDto>> GetProductsAsync(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
        }

        var products = await _productRepository.GetRestaurantProductsAsync(restaurantId);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return productDtos;
    }
}
