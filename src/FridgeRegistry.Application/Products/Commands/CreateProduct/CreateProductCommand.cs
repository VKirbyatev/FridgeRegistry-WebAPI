using MediatR;

namespace FridgeRegistry.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public Guid? CategoryId { get; set; } 
    
    public string Name { get; set; } = null!;
        
    public string Description { get; set; } = null!;
    
    public string ShelfLife { get; set; } = null!;
}