using Resources.Interfaces.IRepository;

using System.Data;
using Resources.Exceptions;

namespace Logic;

public class ProductService(IProductRepository productRepository)
{
    IProductRepository _productRepository = productRepository;

    public List<Product> GetProduct(int? id = null)
    {
        if (id == null)
            return _productRepository.GetAll();
        return [_productRepository.GetWhere(p => p.Id == id).FirstOrDefault()];
    }

    public void AddProduct(Product product)
    {
        if (!CheckProduct(product))
            return;
        if (ProductNameAlreadyExists(product))
            throw new DuplicateNameException("Product already exists");

        try
        {
            _productRepository.Create(product);
        }
        catch (Exception e)
        {
            throw new DataException("Error adding product", e);
        }
    }

    private bool ProductNameAlreadyExists(Product product)
    {
        return _productRepository.GetWhere(p => p.Title == product.Title).Any();
    }

    private bool CheckProduct(Product product)
    {
        if (product.Title.Length > 255)
            throw new InvalidLenghtException("Title is too long");
        if (product.ThumbnailImage.Length > 255)
            throw new InvalidLenghtException("Thumbnail image path is too long");
        if (product.Stock < 0)
            throw new FormatException("Stock can't be negative");
        return true;
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetWhere(p => p.Id == id).FirstOrDefault();
    }
}