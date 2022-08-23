using FridgeRegistry.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Identity.Common.Initializations;

public static class IdentityDbContextInitialization
{
    public static IServiceCollection AddIdentityDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        
        services.AddDbContext<IdentityDatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDatabaseContext>();

        return services;
    }
}