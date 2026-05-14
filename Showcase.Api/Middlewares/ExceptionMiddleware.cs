using Showcase.Core.Exceptions;

namespace Showcase.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate requestDelegate)
{

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            MainNotFoundException => StatusCodes.Status404NotFound,
            MainException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            StatusCode = statusCode,
            Message = ex.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
}