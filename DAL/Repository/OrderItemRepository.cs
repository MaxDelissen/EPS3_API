using Resources.Models.DbModels;

namespace DAL.Repository;

public class OrderItemRepository : DirectDbRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {

    }
}