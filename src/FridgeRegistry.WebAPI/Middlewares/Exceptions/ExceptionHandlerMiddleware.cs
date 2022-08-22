using System.Net;
using System.Text.Json;
using FridgeRegistry.Application.Common.Exceptions;

namespace FridgeRegistry.WebAPI.Middlewares.Exceptions;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;   
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = String.Empty;

        switch (exception)
        {
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (message == String.Empty)
        {
            message = JsonSerializer.Serialize(new
            {
                Error = exception.Message
            });
        }

        await context.Response.WriteAsync(message);
    }
}