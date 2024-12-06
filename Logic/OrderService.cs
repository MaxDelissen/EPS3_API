using System.Data;
using Resources.Exceptions;
using Resources.Models.DbModels;

namespace Logic;

public class OrderService(IOrderRepository orderRepository)
{
	public List<Order> GetSellerOrders(int sellerId)
	{
		var orders = orderRepository.GetWhere(order => order.SellerId == sellerId);
		if (orders == null)
		{
			throw new NotFoundException("Orders not found");
		}

		return orders;
	}
}