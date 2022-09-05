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

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!_redisCacheConfiguration.IsEnabled)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
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

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(exception.Message));
        }
    }
}