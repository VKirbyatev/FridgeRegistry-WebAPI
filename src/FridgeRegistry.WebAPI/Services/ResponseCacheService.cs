using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FridgeRegistry.WebAPI.Services;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDistributedCache _distributedCache;

    public ResponseCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheResponseAsync(string cacheKey, object? responseObject, TimeSpan cacheLifeTime)
    {
        if (responseObject == null)
        {
            return;
        }

        var serializedResponse = JsonConvert.SerializeObject(responseObject);
        var cacheOptions = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = cacheLifeTime
        };
        
        await _distributedCache.SetStringAsync(cacheKey, serializedResponse, cacheOptions);
    }

    public async Task<string?> GetCachedResponseAsync(string cacheKey)
    {
        var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);

        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }
}