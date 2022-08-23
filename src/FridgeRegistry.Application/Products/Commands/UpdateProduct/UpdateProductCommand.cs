using MediatR;

namespace FridgeRegistry.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }
        
    public string Description { get; set; }
    
    public string ShelfLife { get; set; }
}