using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Logic;
using Moq;
using Resources.Exceptions;
using Resources.Interfaces.IRepository;
using Resources.Models.DbModels;
using Xunit;

namespace Tests;

[TestSubject(typeof(ProductService))]
public class ProductServiceTest
{
	private readonly Mock<IProductRepository> _productRepositoryMock;
	private readonly ProductService _productService;

	public ProductServiceTest()
	{
		_productRepositoryMock = new Mock<IProductRepository>();
		_productService = new ProductService(_productRepositoryMock.Object);
	}

	[Fact]
	public void GetProduct_ReturnsAllProducts_WhenIdIsNull()
	{
		var products = new List<Product>
		{
			new()
				{ Id = 1, Title = "Product1" }
		};
		_productRepositoryMock.Setup(repo => repo.GetAll()).Returns(products);

		var result = _productService.GetProduct();

		Assert.Equal(products, result);
	}

	[Fact]
	public void GetProduct_ReturnsProduct_WhenIdIsValid()
	{
		var product = new Product { Id = 1, Title = "Product1" };
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product> { product });

		var result = _productService.GetProduct(1);

		Assert.Single(result);
		Assert.Equal(product, result.First());
	}

	[Fact]
	public void GetProduct_ThrowsDataException_WhenProductNotFound()
	{
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

		Assert.Throws<DataException>(() => _productService.GetProduct(1));
	}

	[Fact]
	public void AddProduct_AddsProduct_WhenProductIsValid()
	{
		var product = new Product { Id = 1, Title = "Product1", ThumbnailImage = "image.jpg", Stock = 10 };
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

		_productService.AddProduct(product);

		_productRepositoryMock.Verify(expression: repo => repo.Create(product), Times.Once);
	}

	[Fact]
	public void AddProduct_ThrowsDuplicateNameException_WhenProductNameExists()
	{
		var product = new Product { Id = 1, Title = "Product1", ThumbnailImage = "image.jpg", Stock = 10 };
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product> { product });

		Assert.Throws<DuplicateNameException>(() => _productService.AddProduct(product));
	}

	[Fact]
	public void AddProduct_ThrowsInvalidLenghtException_WhenTitleIsTooLong()
	{
		var product = new Product { Id = 1, Title = new string('a', 256), ThumbnailImage = "image.jpg", Stock = 10 };

		Assert.Throws<InvalidLenghtException>(() => _productService.AddProduct(product));
	}

	[Fact]
	public void AddProduct_ThrowsInvalidLenghtException_WhenThumbnailImagePathIsTooLong()
	{
		var product = new Product { Id = 1, Title = "Product1", ThumbnailImage = new string('a', 256), Stock = 10 };

		Assert.Throws<InvalidLenghtException>(() => _productService.AddProduct(product));
	}

	[Fact]
	public void AddProduct_ThrowsFormatException_WhenStockIsNegative()
	{
		var product = new Product { Id = 1, Title = "Product1", ThumbnailImage = "image.jpg", Stock = -1 };

		Assert.Throws<FormatException>(() => _productService.AddProduct(product));
	}

	[Fact]
	public void GetProductById_ReturnsProduct_WhenIdIsValid()
	{
		var product = new Product { Id = 1, Title = "Product1" };
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product> { product });

		Product result = _productService.GetProductById(1);

		Assert.Equal(product, result);
	}

	[Fact]
	public void GetProductByTitle_ReturnsProducts_WhenTitleMatches()
	{
		var products = new List<Product>
		{
			new()
				{ Id = 1, Title = "Product1" }
		};
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);

		var result = _productService.GetProductByTitle("Product");

		Assert.Equal(products, result);
	}

	[Fact]
	public void GetStock_ReturnsStock_WhenProductIdIsValid()
	{
		var product = new Product { Id = 1, Stock = 10 };
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product> { product });

		int? result = _productService.GetStock(1);

		Assert.Equal(10, result);
	}

	[Fact]
	public void DeleteProduct_DeletesProduct_WhenProductIsValid()
	{
		var product = new Product { Id = 1, Title = "Product1" };

		_productService.DeleteProduct(product);

		_productRepositoryMock.Verify(expression: repo => repo.Delete(product), Times.Once);
	}

	[Fact]
	public void UpdateStock_UpdatesStock_WhenProductIsValid()
	{
		var product = new Product { Id = 1, Stock = 10 };

		_productService.UpdateStock(product, 20);

		Assert.Equal(20, product.Stock);
		_productRepositoryMock.Verify(expression: repo => repo.Update(product), Times.Once);
	}

	[Fact]
	public void GetUserProducts_ReturnsProducts_WhenUserIdIsValid()
	{
		var products = new List<Product>
		{
			new()
				{ Id = 1, SellerId = 1, Title = "Product1" }
		};
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(products);

		var result = _productService.GetUserProducts(1);

		Assert.Equal(products, result);
	}

	[Fact]
	public void GetUserProducts_ThrowsDataException_WhenNoProductsFound()
	{
		_productRepositoryMock.Setup(repo => repo.GetWhere(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

		Assert.Throws<DataException>(() => _productService.GetUserProducts(1));
	}
}