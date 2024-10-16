namespace Resources.Exceptions;

public class InvalidLenghtException : Exception
{
    public InvalidLenghtException() {}

    public InvalidLenghtException(string message) : base(message) {}

    public InvalidLenghtException(string message, Exception innerException) : base(message, innerException) {}
}