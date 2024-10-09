using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resources.Interfaces;
using Resources.Models;

namespace DAL;

public class ProductRepository : IProductRepository
{
    private AppDbContext _context;

    public ProductRepository(IConfiguration configuration)
    {
        _context = new AppDbContext(new AppConfiguration(configuration));
    }

    public List<Product> GetAllProducts() => _context.Products.ToList();
}