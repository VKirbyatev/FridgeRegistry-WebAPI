namespace FridgeRegistry.Identity.Contracts.Responses;

public class AuthSuccessResponse
{
    public string AccessToken { get; set; }
    
    public Guid RefreshToken { get; set; }
}