using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IEnumerable<Product> Products { get; set; }

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Products = _dbContext.Products;
    }

    public async Task AddProductAsync(Product product)
    {
       await _dbContext.Products.AddAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
       var product = await _dbContext.Products.FindAsync(productId);
       if(product is null)
       {
            return false;
       }
        _dbContext.Products.Remove(product);
        return true;
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
}
