using FluentValidation;
using FluentValidation.AspNetCore;
using FridgeRegistry.Identity.Common.Validators;

namespace FridgeRegistry.Identity.Common.Initializations;

public static class ValidationInitialization
{
    public static IServiceCollection AddCustomValidation(this IServiceCollection services)
    {
        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());
        
        return services;
    } 
}