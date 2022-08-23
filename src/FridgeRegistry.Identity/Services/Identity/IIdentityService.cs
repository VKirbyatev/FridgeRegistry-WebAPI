using FridgeRegistry.Identity.Models;

namespace FridgeRegistry.Identity.Services.Identity;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string email, string password);
    
    Task<AuthenticationResult> LoginAsync(string email, string password);
    
    Task<AuthenticationResult> RefreshTokenAsync(string accessToken, Guid refreshToken);
    
    Task<AuthenticationResult> Logout(string accessToken, Guid refreshToken);
}