namespace Showcase.Core.Exceptions.ApiExceptions;

public class ApiException : Exception
{

    public ApiException(string? errorMessage) : base(errorMessage)
    {

    }

    public ApiException(int statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}