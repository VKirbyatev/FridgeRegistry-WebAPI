using FridgeRegistry.Domain.Common.Enums;

namespace FridgeRegistry.WebAPI.Contracts.Requests.UserProduct;

public class UpdateUserProductRequest
{
    public float? Quantity { get; set; }
    public QuantityType? QuantityType { get; set; }
    
    public DateTime? ProductionDate { get; set; }
    
    public string? Commentary { get; set; }
}