using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Products.Rules;

public partial class ProductNameMaxLengthRule : IBusinessRule
{
    private readonly string _name;
    private const byte NameMaxLength = 50;
    
    public ProductNameMaxLengthRule(string name)
    {
        _name = name;
    }
    
    public bool IsBroken()
    {
        return _name.Length > NameMaxLength;
    }

    public string Message => $"The Name's property length must not exceed {NameMaxLength} characters";
}