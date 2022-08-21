using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Common;
using FridgeRegistry.Domain.Products.Rules;

namespace FridgeRegistry.Domain.Products;

public class Product : Entity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public ShelfLife ShelfLife { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }
    
    private Product() {}

    public Product(
        string name,
        string description,
        uint shelfLife
        )
    {
        CheckRule(new ProductNameMaxLengthRule(name));
        
        Id = Guid.NewGuid();

        Name = name;
        Description = description;

        ShelfLife = new ShelfLife(shelfLife);

        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }
}