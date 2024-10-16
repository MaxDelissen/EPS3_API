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
}