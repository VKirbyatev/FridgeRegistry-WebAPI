using System.Net.Http.Headers;
using System.Net.Http.Json;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Identity.Contracts;
using FridgeRegistry.Identity.Contracts.Requests;
using FridgeRegistry.Identity.Models;
using FridgeRegistry.Infrastructure.Persistence;
using FridgeRegistry.Tests.IntegrationTests.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FridgeRegistry.Tests.IntegrationTests;

public class IntegrationTest
{
    private readonly HttpClient _identityClient;

    protected IntegrationTest()
    {
        var identityAppFactory = new WebApplicationFactory<IdentityProgram>();
        
        _identityClient = identityAppFactory.CreateClient();
    }

    protected static HttpClient CreateWebApiClient()
    {
        var dbName = Guid.NewGuid().ToString();
        
        var webApiAppFactory = new WebApplicationFactory<WebApiProgram>()
            .WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll<DbContextOptions<FridgeRegistryDbContext>>();
                        services.RemoveAll<IDbContext>();

                        services.AddDbContext<FridgeRegistryDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(dbName);
                        });
                        services.AddScoped<IDbContext>(provider => provider.GetService<FridgeRegistryDbContext>() ?? throw new InvalidOperationException());
                        services.AddEntityFrameworkInMemoryDatabase();
                    });
                }
            );
        
        return webApiAppFactory.CreateClient();
    }

    protected async Task AuthenticateAsync(HttpClient client, AuthenticationRoles role = AuthenticationRoles.Admin)
    {
        var jwt = role switch
        {
            AuthenticationRoles.Admin => await GetAdminJwtAsync(),
            AuthenticationRoles.BasicUser => await GetBasicUserJwtAsync(),
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, "Wrong authentication role")
        };

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "bearer",
            jwt
        );
    }

    private async Task<string> GetAdminJwtAsync()
    {
        var response = await _identityClient.PostAsJsonAsync(
            ApiRoutes.Identity.Login,
            new UserLoginRequest()
            {
                Email = "admin@mail.ru",
                Password = "AdminPassword123!"
            }
        );

        var registrationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

        return registrationResponse!.AccessToken;
    }
    
    private async Task<string> GetBasicUserJwtAsync()
    {
        var response = await _identityClient.PostAsJsonAsync(
            ApiRoutes.Identity.Login,
            new UserLoginRequest()
            {
                Email = "user@mail.ru",
                Password = "UserPassword123!"
            }
        );

        var registrationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

        return registrationResponse!.AccessToken;
    }
}