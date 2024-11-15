using Resources.DTOs;
using Resources.Models.DbModels;

namespace Resources.Interfaces.IRepository;

public interface IOrderRepository : IDirectDbRepository<Order>
{
    int CreateCartFromDto(OrderDto cart);
}