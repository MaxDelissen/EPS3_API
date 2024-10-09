using Resources.Models;

namespace Resources.Interfaces;

public interface IProductRepository
{
    List<Product> GetAllProducts();
}