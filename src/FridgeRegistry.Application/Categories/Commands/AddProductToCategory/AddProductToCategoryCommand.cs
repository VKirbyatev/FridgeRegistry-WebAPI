using MediatR;

namespace FridgeRegistry.Application.Categories.Commands.AddProductToCategory;

public class AddProductToCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
    
    public Guid ProductId { get; set; }
}