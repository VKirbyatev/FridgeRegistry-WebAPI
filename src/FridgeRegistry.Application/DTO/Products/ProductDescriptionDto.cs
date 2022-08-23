namespace FridgeRegistry.Application.DTO.Products;

public class ProductDescriptionDto
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    
    public string ShelfLife { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}