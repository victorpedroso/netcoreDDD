using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace TechSolutions.Infrastructure.Data.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";

        if (exception is Exception)
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = exception.Message;
        }

        response.StatusCode = (int)statusCode;
        var errorResponse = new { message };
        var jsonResponse = JsonSerializer.Serialize(errorResponse);

        return response.WriteAsync(jsonResponse);
    }
}
