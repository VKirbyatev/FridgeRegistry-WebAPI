using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Categories.Rules;

public class CategoryNameMaxLengthRule : IBusinessRule
{
    private readonly string _name;
    private const byte NameMaxLength = 100;
    
    public CategoryNameMaxLengthRule(string name)
    {
        _name = name;
    }
    
    public bool IsBroken()
    {
        return _name.Length > NameMaxLength;
    }

    public string Message => $"The Category Name's length must not exceed {NameMaxLength} characters";
}