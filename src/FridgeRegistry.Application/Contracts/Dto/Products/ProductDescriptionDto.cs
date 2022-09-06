namespace FridgeRegistry.Application.Contracts.Dto.Products;

public class ProductDescriptionDto
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public string ShelfLife { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}