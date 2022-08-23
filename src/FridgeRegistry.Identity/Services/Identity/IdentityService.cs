using FridgeRegistry.Identity.Common.Constants;
using FridgeRegistry.Identity.Data;
using FridgeRegistry.Identity.Models;
using FridgeRegistry.Identity.Services.Jwt;
using Microsoft.AspNetCore.Identity;

namespace FridgeRegistry.Identity.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityDatabaseContext _dbContext; 
    
    private readonly IJwtService _jwtService;

    public IdentityService(
        UserManager<IdentityUser> userManager,
        IdentityDatabaseContext dbContext,
        
        IJwtService jwtService
    )
    {
        _userManager = userManager;
        _dbContext = dbContext;

        _jwtService = jwtService;
    }

    public async Task<AuthenticationResult> RegisterAsync(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email.ToLower());

        if (existingUser != null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new []{ "User with this email already exists" }
            };
        }

        var user = new IdentityUser()
        {
            Email = email,
            UserName = email,
        };

        var createdUser = await _userManager.CreateAsync(user, password);

        if (!createdUser.Succeeded)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = createdUser.Errors.Select(error => error.Description)
            };
        }

        await _userManager.AddToRoleAsync(user, Roles.BasicUser);

        return await _jwtService.GenerateJwtTokensPair(user);
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email.ToLower());

        if (user == null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new []{ "User with this credentials does not exists" }
            };
        }

        var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!userHasValidPassword)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new[] { "User with this credentials does not exists" }
            };
        }

        return await _jwtService.GenerateJwtTokensPair(user);
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(string accessToken, Guid refreshToken)
    {
        var storedRefreshToken = await _jwtService.GetStoredRefreshToken(accessToken, refreshToken);

        if (storedRefreshToken == null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new[] { "Invalid token" }
            };
        }
        
        var userId = storedRefreshToken.UserId;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new[] { "This user doesn't exists in the system" }
            };
        }
        
        storedRefreshToken.IsUsed = true;
        await _dbContext.SaveChangesAsync();
        
        return await _jwtService.GenerateJwtTokensPair(user);
    }

    public async Task<AuthenticationResult> Logout(string accessToken, Guid refreshToken)
    {
        var storedRefreshToken = await _jwtService.GetStoredRefreshToken(accessToken, refreshToken);

        if (storedRefreshToken == null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new[] { "Invalid token" }
            };
        }
        
        var userId = storedRefreshToken.UserId;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return new AuthenticationResult()
            {
                ErrorMessages = new[] { "This user doesn't exists in the system" }
            };
        }

        storedRefreshToken.IsInvalidated = true;
        await _dbContext.SaveChangesAsync();

        return new AuthenticationResult()
        {
            Success = true,
        };
    }
}