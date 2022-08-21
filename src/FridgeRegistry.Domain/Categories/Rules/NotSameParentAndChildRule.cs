using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Categories.Rules;

public class NotSameParentAndChildRule : IBusinessRule
{
    private readonly Category _parent;
    private readonly Category _child;
    
    public NotSameParentAndChildRule(Category parent, Category child)
    {
        _parent = parent;
        _child = child;
    }
    
    public bool IsBroken()
    {
        return _parent.Id == _child.Id;
    }

    public string Message => "Parent class can't be child for itself";
}