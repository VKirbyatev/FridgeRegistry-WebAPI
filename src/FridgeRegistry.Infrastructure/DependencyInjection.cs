using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeRegistry.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<FridgeRegistryDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IDbContext>(provider => provider.GetService<FridgeRegistryDbContext>());

        return services;
    }
}