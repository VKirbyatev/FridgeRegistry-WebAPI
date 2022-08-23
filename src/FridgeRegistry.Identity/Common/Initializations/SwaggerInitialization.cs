

using Microsoft.OpenApi.Models;

namespace FridgeRegistry.Identity.Common.Initializations;

public static class SwaggerInitialization
{
    public static IServiceCollection AddSwaggerDocsGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        { 
            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Fridge registry identity API", Version = "v1" });
        });

        return services;
    }
}