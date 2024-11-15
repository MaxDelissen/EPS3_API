using System.Linq.Expressions;
using Resources.DTOs;
using Resources.Models.DbModels;

namespace DAL.Repository;

public class OrderRepository : DirectDbRepository<Order>, IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public int CreateCartFromDto(OrderDto cart)
    {
        var newCart = new Order
        {
            UserId = cart.UserId,
            Status = cart.Status,
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt,
            //ShippingAddressId = cart.ShippingAddressId
        };
        _context.Orders.Add(newCart);
        return _context.SaveChanges();
    }
}