using Resources.Models;

namespace Resources.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts();
    Product? GetProduct(int id);
    void AddProduct(Product product);
}