namespace Logic;

public class ProductService(IProductRepository productRepository)
{
    IProductRepository _productRepository = productRepository;

    public List<Product> GetProducts()
    {
        return _productRepository.GetAllProducts();
    }
}