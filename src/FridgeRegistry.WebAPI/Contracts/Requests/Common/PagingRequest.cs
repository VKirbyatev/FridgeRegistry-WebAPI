namespace FridgeRegistry.WebAPI.Contracts.Requests.Common;

public class PagingRequest
{
    public string? SearchString { get; set; }
    
    public int Take { get; set; }
    
    public int Skip { get; set; }
}