using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Categories.Rules;

public class CategoryNameMaxLengthRule : IBusinessRule
{
    private string Name { get; }
    private const byte NameMaxLength = 100;
    
    public CategoryNameMaxLengthRule(string name)
    {
        Name = name;
    }
    
    public bool IsBroken()
    {
        return Name.Length > NameMaxLength;
    }

    public string Message => $"The Category Name's length must not exceed {NameMaxLength} characters";
}