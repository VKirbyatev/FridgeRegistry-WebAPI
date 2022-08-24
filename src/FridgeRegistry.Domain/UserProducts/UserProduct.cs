using FridgeRegistry.Domain.Common;
using FridgeRegistry.Domain.Common.Enums;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts.Rules;

namespace FridgeRegistry.Domain.UserProducts;

public class UserProduct : Entity
{
    public Guid Id { get; private set; }
    
    public string UserId { get; private set; }
    
    public Product Product { get; private set; }
    
    public string Commentary { get; private set; }
    
    public float Quantity { get; private set; }
    public QuantityType QuantityType { get; private set; }
    
    public DateTime ExpirationDate { get; private set; }
    public DateTime ProductionDate { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }

    private UserProduct() {}

    public UserProduct(
        string userId,
        Product product,
        
        float quantity,
        QuantityType quantityType,
        
        DateTime productionDate,
        string? commentary
        )
    {
        CheckRule(new GreaterThanZeroQuantityRule(quantity));

        var expiryDate = product.ShelfLife.GetExpirationDate(productionDate);
        
        CheckRule(new ExpiryDateAfterNowRule(expiryDate));
        
        Id = Guid.NewGuid();
        
        UserId = userId;
        Product = product;

        Commentary = commentary ?? string.Empty;

        Quantity = quantity;
        QuantityType = quantityType;
        
        ExpirationDate = expiryDate;
        ProductionDate = productionDate;
        
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetProductionDate(DateTime productionDate)
    {
        var expiryDate = Product.ShelfLife.GetExpirationDate(productionDate);
        
        CheckRule(new ExpiryDateAfterNowRule(expiryDate));

        ExpirationDate = expiryDate;
        ProductionDate = productionDate;
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetQuantity(float quantity)
    {
        CheckRule(new GreaterThanZeroQuantityRule(quantity));

        Quantity = quantity;
        
        ModifiedAt = DateTime.UtcNow;
    }
    
    public void SetQuantityType(QuantityType quantityType)
    {
        QuantityType = quantityType;
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetCommentary(string commentary)
    {
        Commentary = commentary;
        
        ModifiedAt = DateTime.UtcNow;
    }
}