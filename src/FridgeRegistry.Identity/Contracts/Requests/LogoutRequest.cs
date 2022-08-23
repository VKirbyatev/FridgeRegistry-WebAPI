namespace FridgeRegistry.Identity.Contracts.Requests;

public class LogoutRequest
{
    public string AccessToken { get; set; }
    
    public Guid RefreshToken { get; set; }
}