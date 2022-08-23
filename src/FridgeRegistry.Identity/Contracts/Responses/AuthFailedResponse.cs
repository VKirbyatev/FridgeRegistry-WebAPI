namespace FridgeRegistry.Identity.Contracts.Responses;

public class AuthFailedResponse
{
    public IEnumerable<string> ErrorMessages { get; set; }
}