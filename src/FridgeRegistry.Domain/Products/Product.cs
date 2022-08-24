using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Common;
using FridgeRegistry.Domain.Products.Rules;

namespace FridgeRegistry.Domain.Products;

public class Product : Entity
{
    public Guid Id { get; private set; }

    public Category? Category { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public ShelfLife ShelfLife { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }
    
    public bool IsDeleted { get; private set; }

    private Product() {}

    public Product(
        string name,
        string description,
        string shelfLife
        )
    {
        CheckRule(new ProductNameMaxLengthRule(name));
        CheckRule(new ShelfLifeFormatRule(shelfLife));
        
        Id = Guid.NewGuid();

        Name = name;
        Description = description;

        var timespan = TimeSpan.Parse(shelfLife);
        ShelfLife = new ShelfLife((int)timespan.TotalMilliseconds);

        IsDeleted = false;
        
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        CheckRule(new ProductNameMaxLengthRule(name));

        Name = name;
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetShelfLife(string shelfLife)
    {
        CheckRule(new ShelfLifeFormatRule(shelfLife));
        
        var timespan = TimeSpan.Parse(shelfLife);
        ShelfLife = new ShelfLife((int)timespan.TotalMilliseconds);
        
        ModifiedAt = DateTime.UtcNow;
    }
    
    public void SetDescription(string description)
    {
        Description = description;
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void Remove()
    {
        IsDeleted = true;
        
        ModifiedAt = DateTime.UtcNow;
    }
}