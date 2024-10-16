using Microsoft.Extensions.Configuration;
using Resources.Interfaces;
using Resources.Interfaces.IRepository;
using Resources.Models;

namespace DAL;

public class ProductRepository : DirectDbRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts() => _context.Products.ToList();
    public Product? GetProduct(int id) => _context.Products.FirstOrDefault(p => p.Id == id);
    public void AddProduct(Product product)
    {
        try
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}