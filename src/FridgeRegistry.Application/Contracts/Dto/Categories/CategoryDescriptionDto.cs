namespace FridgeRegistry.Application.Contracts.Dto.Categories;

public class CategoryDescriptionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public Guid? ParentId { get; set; }
    
    public ICollection<CategoryLookupDto> Children { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}