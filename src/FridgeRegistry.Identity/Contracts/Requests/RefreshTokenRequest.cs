namespace FridgeRegistry.Identity.Contracts.Requests;

public class RefreshTokenRequest
{
    public string AccessToken { get; set; }
    
    public Guid RefreshToken { get; set; }
}