using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.UserProducts.Rules;

public class GreaterThanZeroQuantityRule : IBusinessRule
{
    private readonly float _quantity;
    
    public GreaterThanZeroQuantityRule(float quantity)
    {
        _quantity = quantity;
    }
    
    public bool IsBroken()
    {
        return _quantity <= 0;
    }

    public string Message => "Products quantity should be greater than zero";
}