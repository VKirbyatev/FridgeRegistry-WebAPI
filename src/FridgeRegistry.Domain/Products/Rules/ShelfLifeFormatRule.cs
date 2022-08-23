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
        try
        {
            TimeSpan.Parse(_shelfLife);

            return false;
        }
        catch (Exception exception)
        {
            return true;
        }
    }

    public string Message => "Invalid ShelfLife time format";
}