namespace FridgeRegistry.Identity.Common.Configuration;

public class JwtConfiguration
{
    public string Secret { get; set; }
    
    // In seconds
    public int AccessTokenLifeTime { get; set; }
    
    // In seconds
    public int RefreshTokenLifeTime { get; set; }
}