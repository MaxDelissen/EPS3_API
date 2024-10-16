using Resources.Interfaces.IRepository;

namespace Logic;

public class ProductService(IProductRepository productRepository)
{
    IProductRepository _productRepository = productRepository;

    public List<Product> GetProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetWhere(p => p.Id == id).FirstOrDefault();
    }
}