using Resources.Models.DbModels;

namespace Resources.Interfaces.IRepository;

public interface IProductRepository : IDirectDbRepository<Product>
{}