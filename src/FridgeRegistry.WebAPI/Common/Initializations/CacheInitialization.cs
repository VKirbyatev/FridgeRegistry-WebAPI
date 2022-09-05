using FridgeRegistry.WebAPI.Common.Configurations;
using FridgeRegistry.WebAPI.Services;

namespace FridgeRegistry.WebAPI.Common.Initializations;

public static class CacheInitialization
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var redisCacheConfiguration = new RedisCacheConfiguration();
        configuration.GetSection(nameof(RedisCacheConfiguration)).Bind(redisCacheConfiguration);

        services.AddSingleton(redisCacheConfiguration);

        if (!redisCacheConfiguration.IsEnabled)
        {
            return services;
        }

        services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheConfiguration.ConnectionString);
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        return services;
    }
}