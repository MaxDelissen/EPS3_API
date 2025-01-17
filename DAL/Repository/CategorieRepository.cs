using Resources.Interfaces;
using Resources.Models.DbModels;

namespace DAL.Repository;

public class CategorieRepository : DirectDbRepository<ProductCategory>, ICategorieRepository
{
	private readonly AppDbContext _context;

	public CategorieRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}
}