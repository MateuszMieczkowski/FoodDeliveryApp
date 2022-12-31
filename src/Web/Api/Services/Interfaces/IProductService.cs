using Web.Api.Models.ProductDtos;

namespace Web.Api.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync(int restauarantId);

    Task CreateProductAsync(int restaurantId, ProductForUpdateDto dto);
   
    Task DeleteProductAsync(int restaurantId, int productId);
}
