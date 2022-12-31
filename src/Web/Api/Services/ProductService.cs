using AutoMapper;
using Library.Entities;
using Library.Repositories.Interfaces;
using Web.Api.Exceptions;
using Web.Api.Models.ProductDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Services;

public class ProductService : IProductService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IRestaurantRepository restaurantRepository, IProductRepository productRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task CreateProductAsync(int restaurantId, ProductForUpdateDto dto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
        }

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

    public async Task<List<ProductDto>> GetProductsAsync(int restaurantId)
    {
        var restuarant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restuarant is null)
        {
            throw new NotFoundException($"There's no such restaurant with id: {restaurantId}.");
        }

        var products = await _productRepository.GetRestaurantProductsAsync(restaurantId);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return productDtos;
    }
}
