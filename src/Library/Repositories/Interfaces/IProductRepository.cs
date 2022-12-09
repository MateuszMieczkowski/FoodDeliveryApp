using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; set; }

    Task AddProductAsync(Product product);

    void DeleteProduct(Product product);

    Task<Product?> GetProductAsync(int productId);

    Task<List<Product>> GetAllProductsAsync();

    Task<int> SaveChangesAsync();
    IEnumerable<ProductCategory> GetCategories();
}
