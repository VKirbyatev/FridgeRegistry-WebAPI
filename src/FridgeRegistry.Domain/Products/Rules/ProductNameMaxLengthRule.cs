using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Products.Rules;

public partial class ProductNameMaxLengthRule : IBusinessRule
{
    private string Name { get; }
    private const byte NameMaxLength = 50;
    
    public ProductNameMaxLengthRule(string name)
    {
        Name = name;
    }
    
    public bool IsBroken()
    {
        return Name.Length > NameMaxLength;
    }

    public string Message => $"The Name's property length must not exceed {NameMaxLength} characters";
}