using Resources.Models.DbModels;

namespace DAL.Repository;

public class ProductRepository : DirectDbRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}