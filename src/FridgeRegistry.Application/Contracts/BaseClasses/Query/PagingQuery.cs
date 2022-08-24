namespace FridgeRegistry.Application.Contracts.BaseClasses.Query;

public class PagingQuery
{
    public string? SearchString { get; set; }
    
    public int Take { get; set; }
    
    public int Skip { get; set; }
}