namespace FridgeRegistry.Application.DTO.Categories;

public class CategoryDescriptionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid? ParentId { get; set; }
    
    public ICollection<CategoryLookupDto> Children { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}