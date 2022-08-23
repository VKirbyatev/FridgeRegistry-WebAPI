namespace FridgeRegistry.WebAPI.Contracts.Requests.Product;

public class UpdateProductRequest
{
    public string Name { get; set; }
        
    public string Description { get; set; }
    
    public string ShelfLife { get; set; }
}