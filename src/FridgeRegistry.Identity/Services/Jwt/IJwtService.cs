using System.Security.Claims;
using FridgeRegistry.Identity.Data.Entity;
using FridgeRegistry.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FridgeRegistry.Identity.Services.Jwt;

public interface IJwtService
{
    public Task<AuthenticationResult> GenerateJwtTokensPair(IdentityUser user);
    
    public Task<RefreshToken?> GetStoredRefreshToken(string accessToken, Guid refreshToken);

    public ClaimsPrincipal? GetPrincipalFromToken(string token);

    public bool IsJwtWithValidSecureAlgorithm(SecurityToken validatedToken);
}