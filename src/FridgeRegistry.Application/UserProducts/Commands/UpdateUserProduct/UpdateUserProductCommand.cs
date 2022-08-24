using FridgeRegistry.Domain.Common.Enums;
using MediatR;

namespace FridgeRegistry.Application.UserProducts.Commands.UpdateUserProduct;

public class UpdateUserProductCommand : IRequest
{
    public Guid UserProductId { get; set; }
    public string UserId { get; set; }
    
    public string? Commentary { get; set; }
    
    public float? Quantity { get; set; }
    public QuantityType? QuantityType { get; set; }
    
    public DateTime? ProductionDate { get; set; }
}