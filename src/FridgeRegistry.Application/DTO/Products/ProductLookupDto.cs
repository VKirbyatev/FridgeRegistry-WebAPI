namespace FridgeRegistry.Application.DTO.Products;

public class ProductLookupDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string ShelfLife { get; set; }
}