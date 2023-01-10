using Library.Entities;

namespace Library.Repositories.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product>? Products { get; set; }

    Task AddProductAsync(Product product);

    void DeleteProduct(Product product);

    Task<Product?> GetProductAsync(int productId);

    Task<List<Product>> GetAllProductsAsync();

    Task<List<Product>?> GetRestaurantProductsAsync(int restaurantId);

    Task<int> SaveChangesAsync();

    IEnumerable<ProductCategory> GetCategories();
    
}
