using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Logic;
using Moq;
using Resources.Exceptions;
using Resources.Interfaces.IRepository;
using Resources.Models.DbModels;
using Xunit;

namespace Tests;

[TestSubject(typeof(OrderService))]
public class OrderServiceTest
{
	[Fact]
	public void GetSellerOrders_ReturnsOrders_WhenOrdersExist()
	{
		// Arrange
		var mockOrderRepository = new Mock<IOrderRepository>();
		var orderService = new OrderService(mockOrderRepository.Object);
		var sellerId = 1;
		var orders = new List<Order> { new Order { SellerId = sellerId } };
		mockOrderRepository.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns(orders);

		// Act
		var result = orderService.GetSellerOrders(sellerId);

		// Assert
		Assert.Equal(orders, result);
	}

	[Fact]
	public void GetSellerOrders_ThrowsNotFoundException_WhenOrdersDoNotExist()
	{
		// Arrange
		var mockOrderRepository = new Mock<IOrderRepository>();
		var orderService = new OrderService(mockOrderRepository.Object);
		var sellerId = 1;
		mockOrderRepository.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Order, bool>>>())).Returns((List<Order>)null);

		// Act & Assert
		Assert.Throws<NotFoundException>(() => orderService.GetSellerOrders(sellerId));
	}
}