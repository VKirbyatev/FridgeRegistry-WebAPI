namespace FridgeRegistry.Identity.Contracts.Requests;

public class UserRegistrationRequest
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}