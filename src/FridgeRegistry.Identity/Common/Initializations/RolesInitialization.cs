using FridgeRegistry.Identity.Common.Configuration;
using FridgeRegistry.Identity.Common.Constants;
using Microsoft.AspNetCore.Identity;

namespace FridgeRegistry.Identity.Common.Initializations;

public static class RolesInitialization
{
    public static async Task InitializeRoles(WebApplication app, IConfiguration configuration)
    {
        var adminUserConfiguration = new AdminUserConfiguration();
        configuration.Bind(nameof(AdminUserConfiguration), adminUserConfiguration);
        
        var basicUserConfiguration = new BasicUserConfiguration();
        configuration.Bind(nameof(BasicUserConfiguration), basicUserConfiguration);
        
        var serviceScope = app.Services.CreateScope();
 
        var rolesManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        if (!await rolesManager.RoleExistsAsync(Roles.Admin))
        {
            var adminRole = new IdentityRole(Roles.Admin);
            await rolesManager.CreateAsync(adminRole);
        }

        if (!await rolesManager.RoleExistsAsync(Roles.BasicUser))
        {
            var basicUserRole = new IdentityRole(Roles.BasicUser);
            await rolesManager.CreateAsync(basicUserRole);
        }

        var users = userManager.Users.ToList();
        if (!users.Any() && app.Environment.IsDevelopment())
        {
            var admin = new IdentityUser()
            {
                Email = adminUserConfiguration.Email,
                UserName = adminUserConfiguration.UserName,
            };
            
            var basicUser = new IdentityUser()
            {
                Email = basicUserConfiguration.Email,
                UserName = basicUserConfiguration.UserName,
            };

            await userManager.CreateAsync(admin, adminUserConfiguration.Password);
            await userManager.AddToRoleAsync(admin, Roles.Admin);
            
            await userManager.CreateAsync(basicUser, basicUserConfiguration.Password);
            await userManager.AddToRoleAsync(basicUser, Roles.BasicUser);
        }
    }
}