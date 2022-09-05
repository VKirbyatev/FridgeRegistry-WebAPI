using System.Text;
using FridgeRegistry.WebAPI.Common.Configurations;
using FridgeRegistry.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FridgeRegistry.WebAPI.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _cacheLifetimeSeconds;

    public CachedAttribute(int cacheLifetimeSeconds)
    {
        _cacheLifetimeSeconds = cacheLifetimeSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cacheConfigurations = context.HttpContext.RequestServices.GetRequiredService<RedisCacheConfiguration>();

        if (!cacheConfigurations.IsEnabled)
        {
            await next();
            return;
        }
        
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
        var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

        if (!string.IsNullOrEmpty(cacheResponse))
        {
            var contentResult = new ContentResult()
            {
                Content = cacheResponse,
                ContentType = "application/json",
                StatusCode = 200
            };

            context.Result = contentResult;
            return;
        }
        
        var executedContext = await next();

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cacheService.CacheResponseAsync(
                cacheKey,
                okObjectResult.Value,
                TimeSpan.FromSeconds(_cacheLifetimeSeconds)
            );
        }
    }

    private static string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();

        keyBuilder.Append(request.Path);

        var queryParams = request.Query.OrderBy(x => x.Key);

        foreach (var (key, value) in queryParams)
        {
            keyBuilder.Append($"|{key}-{value}");
        }

        return keyBuilder.ToString();
    }
}