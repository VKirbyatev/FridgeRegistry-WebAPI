namespace FridgeRegistry.WebAPI.Contracts.Requests.Category;

public class UpdateCategoryRequest
{
    public Guid? ParentCategoryId { get; set; }
    
    public string? Name { get; set; }
}