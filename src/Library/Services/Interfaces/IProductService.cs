using Library.Models.ProductDtos;

namespace Library.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync(int restaurantId);

    Task CreateProductAsync(int restaurantId, ProductForUpdateDto dto);

    Task DeleteProductAsync(int productId);
}