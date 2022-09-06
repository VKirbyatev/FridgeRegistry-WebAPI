using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Products.Rules;

public class ShelfLifeFormatRule : IBusinessRule
{
    private readonly string _shelfLife;
    
    public ShelfLifeFormatRule(string shelfLife)
    {
        _shelfLife = shelfLife;
    }
    
    public bool IsBroken()
    {
        return !TimeSpan.TryParse(_shelfLife, out _);
    }

    public string Message => "Invalid ShelfLife time format";
}