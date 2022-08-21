using FridgeRegistry.Domain.Categories.Rules;
using FridgeRegistry.Domain.Common;
using FridgeRegistry.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Domain.Categories;

public class Category : Entity
{ 
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public Category? Parent { get; private set; }
    
    private readonly List<Category> _children;
    public IReadOnlyCollection<Category> Children => _children;

    private readonly List<Product> _products;
    public IReadOnlyCollection<Product> Products => _products;

    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }

    private Category() {}

    public Category(string name)
    {
        CheckRule(new CategoryNameMaxLengthRule(name));
     
        Id = Guid.NewGuid();
        Name = name;

        _children = new List<Category>();
        _products = new List<Product>();
        
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetParent(Category parent)
    {
        CheckRule(new NotSameParentAndChildRule(parent, this));
        
        RemoveParent();
        Parent = parent;
        parent._children.Add(this);
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void RemoveParent()
    {
        Parent?._children.RemoveAll(x => x.Id == Id);
        Parent = null;
        
        ModifiedAt = DateTime.UtcNow;
    }
}