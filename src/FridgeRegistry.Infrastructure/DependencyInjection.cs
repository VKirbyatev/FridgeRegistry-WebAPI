using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Infrastructure.Mapping;
using FridgeRegistry.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeRegistry.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<FridgeRegistryDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder => builder.MigrationsAssembly("FridgeRegistry.Infrastructure"));
        });
        
        services.AddScoped<IDbContext>(provider => provider.GetService<FridgeRegistryDbContext>() ?? throw new InvalidOperationException());
        services.AddAutoMapper(options =>
        {
            options.AddProfile(new DomainToDtoProfile());
        });

        return services;
    }
}