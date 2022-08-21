using FridgeRegistry.Domain.Categories.Rules;
using FridgeRegistry.Domain.Common;

namespace FridgeRegistry.Domain.Categories;

public class Category : Entity
{ 
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Category() {}

    public Category(string name)
    {
        CheckRule(new CategoryNameMaxLengthRule(name));
     
        Id = Guid.NewGuid();
        Name = name;
    }
}