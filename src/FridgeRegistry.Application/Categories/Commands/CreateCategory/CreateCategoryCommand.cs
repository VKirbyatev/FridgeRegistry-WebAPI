using MediatR;

namespace FridgeRegistry.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Guid>
{
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
}