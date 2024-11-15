using System.Diagnostics.CodeAnalysis;
using Resources;
using Resources.DTOs;
using Resources.Exceptions;
using Resources.Models.DbModels;

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
        var cartCreated = false;
        Order cart = activeOrders.FirstOrDefault() ?? CreateNewCart(userId, out cartCreated);
        OrderItem? orderItem = !cartCreated ? cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId) : null;

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
            cart.OrderItems.Add(newOrderItem); //Error here!
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

    private void DecreaseItemQuantity(int quantity, OrderItem orderItem)
    {
        orderItem.Quantity -= quantity;
        if (orderItem.Quantity <= 0)
        {
            DeleteOrderItem(orderItem);
        }
        else
        {
            _orderItemRepository.Update(orderItem);
        }
    }

    private void DeleteOrderItem(OrderItem orderItem)
    {
        _orderItemRepository.Delete(orderItem);
    }

    private Order GetUserCart(int userId)
    {
        var activeOrders = GetActiveOrders(userId);
        Order? cart = activeOrders.FirstOrDefault();
        if (cart == null)
            throw new InvalidOperationException("User has no active cart");
        return cart;
    }

    public Product GetProductFromCart(int userId, int productId)
    {
        Order cart = GetUserCart(userId);
        OrderItem? orderItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
        if (orderItem == null)
            throw new ProductNotAvailableException("Product not in cart");

        return _productRepository.GetWhere(p => p.Id == orderItem.ProductId).FirstOrDefault()
               ?? throw new ProductNotAvailableException("Product in cart is no longer available");
    }

    public List<OrderProductDto> GetCart(int userId)
    {
        //Get user's active cart (Should only be one)
        //Get all order items in the cart
        //Get product details for each order item
        //Transform to products to OrderProductDto
        //Return all OrderProductDto
        List<OrderProductDto> orderProducts = new();

        var activeOrders = GetActiveOrders(userId);
        Order? cart = GetUserCart(userId);
        if (cart == null)
            return orderProducts; //Empty cart

        foreach (OrderItem cartOrderItem in cart.OrderItems)
        {
            var product = _productRepository.GetWhere(p => p.Id == cartOrderItem.ProductId).FirstOrDefault();
            if (product == null)
                throw new ProductNotAvailableException("Product in cart is no longer available");

            orderProducts.Add(new OrderProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ThumbnailImage = product.ThumbnailImage,
                Quantity = cartOrderItem.Quantity
            });
        }

        return orderProducts;
    }

    public void RemoveFromCart(int userId, int productId)
    {
        Order cart = GetUserCart(userId);

        OrderItem? orderItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
        if (orderItem == null)
            throw new ProductNotAvailableException("Product not in cart");

        cart.OrderItems.Remove(orderItem);
        _orderItemRepository.Delete(orderItem);
        _orderRepository.Update(cart);
    }

    public void EditProductQuantity(int userId, int productId, int? quantity)
    {
        Order cart = GetUserCart(userId);
        OrderItem? orderItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

        if (orderItem == null)
            throw new ProductNotAvailableException("Product not in cart");

        quantity = CheckQuantity(quantity);
        orderItem.Quantity = quantity.Value;
        _orderItemRepository.Update(orderItem);
    }
}