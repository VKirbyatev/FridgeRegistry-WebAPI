namespace FridgeRegistry.WebAPI.Services;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object? responseObject, TimeSpan timeTimeLive);

    Task<string?> GetCachedResponseAsync(string cacheKey);
}