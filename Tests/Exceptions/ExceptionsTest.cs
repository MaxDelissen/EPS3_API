using System;
using Resources.Exceptions;
using Xunit;

namespace Tests.Exceptions;

//Not the most useful of tests, but for code coverage purposes, it seems good to have them.
public class ExceptionsTest
{
    [Fact]
    public void Constructor_SetsMessageCorrectly()
    {
        var exception = new NotFoundException("Resource not found");
        Assert.Equal("Resource not found", exception.Message);
    }

    [Fact]
    public void Constructor_SetsMessageAndInnerExceptionCorrectly()
    {
        var innerException = new Exception("Inner exception");
        var exception = new NotFoundException("Resource not found", innerException);
        Assert.Equal("Resource not found", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        var exception = new NotFoundException();
        Assert.Equal("Exception of type 'Resources.Exceptions.NotFoundException' was thrown.", exception.Message);
    }

    [Fact]
    public void Constructor_SetsMessageCorrectly_ProductNotAvailable()
    {
        var exception = new ProductNotAvailableException("Product not available");
        Assert.Equal("Product not available", exception.Message);
    }

    [Fact]
    public void Constructor_SetsMessageAndInnerExceptionCorrectly_ProductNotAvailable()
    {
        var innerException = new Exception("Inner exception");
        var exception = new ProductNotAvailableException("Product not available", innerException);
        Assert.Equal("Product not available", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DefaultConstructor_SetsDefaultMessage_ProductNotAvailable()
    {
        var exception = new ProductNotAvailableException();
        Assert.Equal("Exception of type 'Resources.Exceptions.ProductNotAvailableException' was thrown.", exception.Message);
    }

    [Fact]
    public void Constructor_SetsMessageCorrectly_InvalidLength()
    {
        var exception = new InvalidLenghtException("Invalid length");
        Assert.Equal("Invalid length", exception.Message);
    }

    [Fact]
    public void Constructor_SetsMessageAndInnerExceptionCorrectly_InvalidLength()
    {
        var innerException = new Exception("Inner exception");
        var exception = new InvalidLenghtException("Invalid length", innerException);
        Assert.Equal("Invalid length", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DefaultConstructor_SetsDefaultMessage_InvalidLength()
    {
        var exception = new InvalidLenghtException();
        Assert.Equal("Exception of type 'Resources.Exceptions.InvalidLenghtException' was thrown.", exception.Message);
    }
}