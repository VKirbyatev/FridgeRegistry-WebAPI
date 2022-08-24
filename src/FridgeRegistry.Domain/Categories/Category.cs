using FridgeRegistry.Domain.Categories.Rules;
using FridgeRegistry.Domain.Common;
using FridgeRegistry.Domain.Products;

namespace FridgeRegistry.Domain.Categories;

public class Category : Entity
{ 
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public Category? Parent { get; private set; }
    
    private readonly List<Category> _children = new List<Category>();
    public IReadOnlyCollection<Category> Children => _children;

    private readonly List<Product> _products = new List<Product>();
    public IReadOnlyCollection<Product> Products => _products;

    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }
    
    public bool IsDeleted { get; private set; }

    private Category() {}

    public Category(string name)
    {
        CheckRule(new CategoryNameMaxLengthRule(name));
     
        Id = Guid.NewGuid();
        Name = name;

        _children = new List<Category>();
        _products = new List<Product>();

        IsDeleted = false;
        
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        CheckRule(new CategoryNameMaxLengthRule(name));
        
        Name = name;
        
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

    public void AddProduct(Product product)
    {
        CheckRule(new UniqueProductInCategoryRule(this, product));
        
        _products.Add(product);
        
        ModifiedAt = DateTime.UtcNow;
    }
    
    public void RemoveProduct(Product product)
    {
        var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);

        if (existingProduct != null)
        {
            _products.Remove(existingProduct);
        }
        
        ModifiedAt = DateTime.UtcNow;
    }

    public void Remove()
    {
        IsDeleted = true;
        ModifiedAt = DateTime.UtcNow;
    }
}