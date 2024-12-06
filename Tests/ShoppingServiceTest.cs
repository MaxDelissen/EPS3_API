using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Logic;
using Moq;
using Resources;
using Resources.Exceptions;
using Resources.Interfaces.IRepository;
using Resources.Models.DbModels;
using Xunit;

namespace Tests;

[TestSubject(typeof(ShoppingService))]
public class ShoppingServiceTest
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly ShoppingService _shoppingService;

    public ShoppingServiceTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _shoppingService = new ShoppingService(
            _productRepositoryMock.Object,
            _orderRepositoryMock.Object,
            _orderItemRepositoryMock.Object,
            _addressRepositoryMock.Object);
    }

    [Fact]
    public void AddToCart_AddsNewItemToCart()
    {
        var userId = 1;
        var productId = 1;
        var quantity = 2;
        var product = new Product { Id = productId, Price = 10.0m };
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem>() };

        _productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product> { product });
        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        _shoppingService.AddToCart(userId, productId, quantity);

        _orderItemRepositoryMock.Verify(repo => repo.Create(It.IsAny<OrderItem>()), Times.Once);
        _orderRepositoryMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void AddToCart_IncreasesQuantityOfExistingItem()
    {
        var userId = 1;
        var productId = 1;
        var quantity = 2;
        var orderItem = new OrderItem { ProductId = productId, Quantity = 1, Price = 10.0m };
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem> { orderItem } };

        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        _shoppingService.AddToCart(userId, productId, quantity);

        Assert.Equal(3, orderItem.Quantity);
        _orderItemRepositoryMock.Verify(repo => repo.Update(It.IsAny<OrderItem>()), Times.Once);
        _orderRepositoryMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void AddToCart_ThrowsException_WhenProductNotFound()
    {
        var userId = 1;
        var productId = 1;
        var quantity = 2;

        _productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());
        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order>());

        Assert.Throws<InvalidOperationException>(() => _shoppingService.AddToCart(userId, productId, quantity));
    }

    [Fact]
    public void RemoveFromCart_RemovesItemFromCart()
    {
        var userId = 1;
        var productId = 1;
        var orderItem = new OrderItem { ProductId = productId, Quantity = 1, Price = 10.0m };
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem> { orderItem } };

        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        _shoppingService.RemoveFromCart(userId, productId);

        Assert.Empty(order.OrderItems);
        _orderItemRepositoryMock.Verify(repo => repo.Delete(It.IsAny<OrderItem>()), Times.Once);
        _orderRepositoryMock.Verify(repo => repo.Update(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void RemoveFromCart_ThrowsException_WhenProductNotInCart()
    {
        var userId = 1;
        var productId = 1;
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem>() };

        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        Assert.Throws<ProductNotAvailableException>(() => _shoppingService.RemoveFromCart(userId, productId));
    }

    [Fact]
    public void EditProductQuantity_UpdatesQuantity()
    {
        var userId = 1;
        var productId = 1;
        var quantity = 5;
        var orderItem = new OrderItem { ProductId = productId, Quantity = 1, Price = 10.0m };
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem> { orderItem } };

        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        _shoppingService.EditProductQuantity(userId, productId, quantity);

        Assert.Equal(quantity, orderItem.Quantity);
        _orderItemRepositoryMock.Verify(repo => repo.Update(It.IsAny<OrderItem>()), Times.Once);
    }

    [Fact]
    public void EditProductQuantity_ThrowsException_WhenProductNotInCart()
    {
        var userId = 1;
        var productId = 1;
        var quantity = 5;
        var order = new Order { UserId = userId, Status = OrderStatus.NotOrdered, OrderItems = new List<OrderItem>() };

        _orderRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(new List<Order> { order });

        Assert.Throws<ProductNotAvailableException>(() => _shoppingService.EditProductQuantity(userId, productId, quantity));
    }
}