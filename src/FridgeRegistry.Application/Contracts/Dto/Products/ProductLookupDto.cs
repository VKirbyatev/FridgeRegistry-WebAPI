namespace FridgeRegistry.Application.Contracts.Dto.Products;

public class ProductLookupDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShelfLife { get; set; } = null!;
}