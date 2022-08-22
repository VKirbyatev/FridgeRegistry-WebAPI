using MediatR;

namespace FridgeRegistry.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
}