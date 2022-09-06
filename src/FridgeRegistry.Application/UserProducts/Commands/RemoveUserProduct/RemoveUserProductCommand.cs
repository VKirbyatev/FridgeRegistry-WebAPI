using MediatR;

namespace FridgeRegistry.Application.UserProducts.Commands.RemoveUserProduct;

public class RemoveUserProductCommand : IRequest
{
    public Guid UserProductId { get; set; }
    
    public string UserId { get; set; } = null!;
}