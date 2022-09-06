using FridgeRegistry.Domain.Common.Enums;

namespace FridgeRegistry.Application.Contracts.Dto.UserProduct;

public class UserProductLookupDto
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = null!;
    
    public float Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public DateTime ProductionDate { get; set; }
}