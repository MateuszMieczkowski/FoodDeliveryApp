using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Library.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IQueryable<Product>? Products { get; set; }

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Products = _dbContext.Products;
    }

    public async Task AddProductAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }

    public void DeleteProduct(Product product)
    {
        if (product is null)
        {
            return;
        }
        _dbContext.Products.Remove(product);
    }

    public async Task<Product?> GetProductAsync(int productId)
    {
        return await _dbContext.Products.FindAsync(productId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        return products;
    }

    public IQueryable<ProductCategory> GetCategories()
    {
        return _dbContext.ProductCategories;
    }

    public async Task<List<Product>?> GetRestaurantProductsAsync(int restaurantId)
    {
        var products = await _dbContext.Products.Where(r => r.RestaurantId == restaurantId)
                                                 .ToListAsync();
        return products;
    }
}
