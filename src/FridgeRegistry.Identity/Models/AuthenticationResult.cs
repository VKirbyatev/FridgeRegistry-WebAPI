namespace FridgeRegistry.Identity.Models;

public class AuthenticationResult
{
    public string AccessToken { get; set; }
    
    public Guid RefreshToken { get; set; }
    
    public bool Success { get; set; }
    
    public IEnumerable<string> ErrorMessages { get; set; }
}