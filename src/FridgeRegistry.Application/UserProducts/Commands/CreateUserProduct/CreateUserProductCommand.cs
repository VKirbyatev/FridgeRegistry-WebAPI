using FridgeRegistry.Domain.Common.Enums;
using MediatR;

namespace FridgeRegistry.Application.UserProducts.Commands.CreateUserProduct;

public class CreateUserProductCommand : IRequest<Guid>
{
    public string UserId { get; set; }
        
    public Guid ProductId { get; set; }
        
    public float Quantity { get; set; }
        
    public QuantityType QuantityType { get; set; }
        
    public DateTime? ProductionDate { get; set; }
    
    public string? Commentary { get; set; }
}