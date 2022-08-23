using FridgeRegistry.Domain.Common.Interfaces;
using FridgeRegistry.Domain.Products;

namespace FridgeRegistry.Domain.Categories.Rules;

public class UniqueProductInCategoryRule : IBusinessRule
{
    private readonly Category _category;
    private readonly Product _product;
    
    public UniqueProductInCategoryRule(Category category, Product product)
    {
        _category = category;
        _product = product;
    }
    
    public bool IsBroken()
    {
        return _category.Products.FirstOrDefault(product => product.Id == _product.Id) != null;
    }

    public string Message => "Product can't be placed in the same category twice";
}