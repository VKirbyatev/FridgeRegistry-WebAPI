using System.Text;
using FridgeRegistry.Identity.Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FridgeRegistry.Identity.Common.Initializations;

public static class JwtIdentityInitialization
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        
        configuration.Bind(nameof(JwtConfiguration), jwtConfiguration);

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true,
        };
        
        services.AddSingleton(jwtConfiguration);
        services.AddSingleton(tokenValidationParameters);

        return services;
    }
}