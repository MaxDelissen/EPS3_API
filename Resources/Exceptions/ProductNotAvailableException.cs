namespace Resources.Exceptions;

public class ProductNotAvailableException : Exception
{
    public ProductNotAvailableException()
    {
    }

    public ProductNotAvailableException(string message) : base(message)
    {
    }

    public ProductNotAvailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}