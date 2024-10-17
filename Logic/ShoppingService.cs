using System.Diagnostics.CodeAnalysis;
using Resources;
using Resources.DTOs;

namespace Logic;

public class ShoppingService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IAddressRepository _addressRepository;

    public ShoppingService(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IAddressRepository addressRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _addressRepository = addressRepository;
    }

    public void AddToCart(int userId, int productId, int? quantity)
    {
        quantity = CheckQuantity(quantity);

        var activeOrders = GetActiveOrders(userId);
        bool cartCreated = false;
        var cart = activeOrders.FirstOrDefault() ?? CreateNewCart(userId, out cartCreated);
        var orderItem = !cartCreated ? cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId) : null;

        if (orderItem != null)
        {
            IncreaseItemQuantity(quantity.Value, orderItem);
        }
        else
        {
            var product = _productRepository.GetWhere(p => p.Id == productId).FirstOrDefault()
                          ?? throw new InvalidOperationException("Product not found");

            var newOrderItem = new OrderItem
            {
                ProductId = productId,
                Quantity = quantity.Value,
                Price = product.Price
            };
            cart.OrderItems.Add(newOrderItem);
            _orderItemRepository.Create(newOrderItem);
        }
        _orderRepository.Update(cart);
    }

    [return: NotNull] private static int? CheckQuantity(int? quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");

        return quantity ?? 1;
    }

    private List<Order> GetActiveOrders(int userId)
    {
        var activeOrders = _orderRepository.GetWhere(o => o.UserId == userId && o.Status == OrderStatus.NotOrdered);
        if (activeOrders.Count > 1)
            throw new InvalidOperationException("User has multiple active carts");

        return activeOrders;
    }

    private Order CreateNewCart(int userId, out bool cartCreated)
    {
        var cart = new OrderDto()
        {
            UserId = userId,
            Status = OrderStatus.NotOrdered,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ShippingAddressId = 1 //For debugging purposes
        };
        int newCartId = _orderRepository.CreateCartFromDto(cart);
        cartCreated = true;
        return _orderRepository.GetWhere(o => o.Id == newCartId).FirstOrDefault();
    }

    private void IncreaseItemQuantity(int quantity, OrderItem orderItem)
    {
        orderItem.Quantity += quantity;
        _orderItemRepository.Update(orderItem);
    }
}