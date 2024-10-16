using Resources.Models;

namespace Resources.Interfaces.IRepository;

public interface IProductRepository : IDirectDbRepository<Product>
{
    List<Product> GetAllProducts();
}