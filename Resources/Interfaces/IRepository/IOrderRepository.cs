using Resources.DTOs;

namespace Resources.Interfaces.IRepository;

public interface IOrderRepository : IDirectDbRepository<Order>
{
    int CreateCartFromDto(OrderDto cart);
}