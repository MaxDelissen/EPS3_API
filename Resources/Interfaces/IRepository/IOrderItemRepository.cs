using Resources.Models.DbModels;

namespace Resources.Interfaces.IRepository;

public interface IOrderItemRepository : IDirectDbRepository<OrderItem>
{

}