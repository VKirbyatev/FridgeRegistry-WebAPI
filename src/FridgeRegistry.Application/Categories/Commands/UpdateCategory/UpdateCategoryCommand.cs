using MediatR;

namespace FridgeRegistry.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
    
    public Guid? ParentCategoryId { get; set; }
    public string Name { get; set; }
}