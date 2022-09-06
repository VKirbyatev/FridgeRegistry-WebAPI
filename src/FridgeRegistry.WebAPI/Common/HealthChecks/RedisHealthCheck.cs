using FridgeRegistry.WebAPI.Common.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace FridgeRegistry.WebAPI.Common.HealthChecks;

public class RedisHealthCheck : IHealthCheck
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly RedisCacheConfiguration _redisCacheConfiguration;

    public RedisHealthCheck(IHttpContextAccessor httpContextAccessor, RedisCacheConfiguration redisCacheConfiguration)
    {
        _httpContextAccessor = httpContextAccessor;
        _redisCacheConfiguration = redisCacheConfiguration;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_redisCacheConfiguration.IsEnabled)
        {
            return HealthCheckResult.Healthy();
        }
        
        try
        {
            var connectionMultiplexer = _httpContextAccessor.HttpContext?.RequestServices.GetService<IConnectionMultiplexer>();
            if (connectionMultiplexer == null)
            {
                throw new Exception("Redis caching is turned off");   
            }

            var database = connectionMultiplexer.GetDatabase();
            database.StringGet("health");

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(exception.Message);
        }
    }
}