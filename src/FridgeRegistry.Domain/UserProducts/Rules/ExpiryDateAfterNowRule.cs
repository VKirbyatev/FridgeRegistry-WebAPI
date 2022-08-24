using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.UserProducts.Rules;

public class ExpiryDateAfterNowRule : IBusinessRule
{
    private readonly DateTime _expiryDate;

    public ExpiryDateAfterNowRule(DateTime expiryDate)
    {
        _expiryDate = expiryDate;
    }
    
    public bool IsBroken()
    {
        return _expiryDate < DateTime.UtcNow;
    }

    public string Message => $"Expiry date should be after {DateTime.UtcNow:f}, provided: {_expiryDate:f}";
}