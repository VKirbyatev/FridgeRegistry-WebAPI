using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FridgeRegistry.Identity.Common.Configuration;
using FridgeRegistry.Identity.Data;
using FridgeRegistry.Identity.Data.Entity;
using FridgeRegistry.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FridgeRegistry.Identity.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly JwtConfiguration _jwtConfiguration;
    private readonly TokenValidationParameters _tokenValidationParameters;

    private readonly IdentityDatabaseContext _dbContext; 

    public JwtService(
        UserManager<IdentityUser> userManager,

        JwtConfiguration jwtConfiguration,
        TokenValidationParameters tokenValidationParameters,
        
        IdentityDatabaseContext dbContext
    )
    {
        _userManager = userManager;

        _jwtConfiguration = jwtConfiguration;
        _tokenValidationParameters = tokenValidationParameters;

        _dbContext = dbContext;
    }
    
    public async Task<AuthenticationResult> GenerateJwtTokensPair(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);

        var userRoles = await _userManager.GetRolesAsync(user);
        
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRoles.First()),
                new Claim("id", user.Id)
            }),
            Expires = DateTime.UtcNow.AddSeconds(_jwtConfiguration.AccessTokenLifeTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken()
        {
            TokenId = Guid.NewGuid(),
            JwtId = accessToken.Id,
            UserId = user.Id,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddSeconds(_jwtConfiguration.RefreshTokenLifeTime),
        };

        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();

        return new AuthenticationResult()
        {
            Success = true,
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = refreshToken.TokenId,
        };
    }
    
    public async Task<RefreshToken?> GetStoredRefreshToken(string accessToken, Guid refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(accessToken);

        if (validatedToken == null)
        {
            return null;
        }

        var jti = validatedToken.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
        var storedRefreshToken =
            await _dbContext.RefreshTokens.SingleOrDefaultAsync(token => token.TokenId == refreshToken);

        if (storedRefreshToken == null
            || DateTime.UtcNow > storedRefreshToken.ExpiryDate
            || storedRefreshToken.IsInvalidated
            || storedRefreshToken.IsUsed
            || storedRefreshToken.JwtId != jti
            )
        {
            return null;
        }

        return storedRefreshToken;
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

            return IsJwtWithValidSecureAlgorithm(validatedToken) ? principal : null;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool IsJwtWithValidSecureAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256);
    }
}