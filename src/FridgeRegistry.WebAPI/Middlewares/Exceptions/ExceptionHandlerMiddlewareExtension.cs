namespace FridgeRegistry.WebAPI.Middlewares.Exceptions;

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}