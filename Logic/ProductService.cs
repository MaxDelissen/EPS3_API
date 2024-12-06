using System.Data;
using Resources.Exceptions;
using Resources.Models.DbModels;

namespace Logic;

public class ProductService(IProductRepository productRepository)
{
    public List<Product> GetProduct(int? id = null)
    {
        if (id == null)
            return productRepository.GetAll();
        return [productRepository.GetWhere(p => p.Id == id).FirstOrDefault() ?? throw new DataException("Product not found")];
    }

    public void AddProduct(Product product)
    {
        CheckProduct(product);
        if (ProductNameAlreadyExists(product))
            throw new DuplicateNameException("Product already exists");

        try
        {
            productRepository.Create(product);
        }
        catch (Exception e)
        {
            throw new DataException("Error adding product", e);
        }
    }

    private bool ProductNameAlreadyExists(Product product)
    {
        return productRepository.GetWhere(p => p.Title == product.Title).Any();
    }

    private void CheckProduct(Product product)
    {
        if (product.Title.Length > 255)
            throw new InvalidLenghtException("Title is too long");
        if (product.ThumbnailImage.Length > 255)
            throw new InvalidLenghtException("Thumbnail image path is too long");
        if (product.Stock < 0)
            throw new FormatException("Stock can't be negative");
    }

    public Product? GetProductById(int id) => productRepository.GetWhere(p => p.Id == id).FirstOrDefault();

    public List<Product> GetProductByTitle(string title) => productRepository.GetWhere(p => p.Title.Contains(title)).ToList();

    public int? GetStock(int productId) => productRepository.GetWhere(p => p.Id == productId).FirstOrDefault()?.Stock;

    public void DeleteProduct(Product product) => productRepository.Delete(product);

    public void UpdateStock(Product product, int stock)
    {
        product.Stock = stock;
        productRepository.Update(product);
    }

    public List<Product> GetUserProducts(int userId)
    {
        var products = productRepository.GetWhere(p => p.SellerId == userId).ToList();
        if (products.Count == 0)
            throw new DataException("No products found");
        return products;
    }
}