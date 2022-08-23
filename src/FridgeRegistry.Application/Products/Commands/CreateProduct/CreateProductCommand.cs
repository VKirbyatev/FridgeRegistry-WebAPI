using MediatR;

namespace FridgeRegistry.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public Guid? CategoryId { get; set; } 
    
    public string Name { get; set; }
        
    public string Description { get; set; }
    
    public string ShelfLife { get; set; }
}