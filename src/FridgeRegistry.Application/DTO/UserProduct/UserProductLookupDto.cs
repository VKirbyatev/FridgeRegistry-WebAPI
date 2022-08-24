using FridgeRegistry.Domain.Common.Enums;

namespace FridgeRegistry.Application.DTO.UserProduct;

public class UserProductLookupDto
{
    public Guid Id { get; set; }
    
    public string ProductName { get; set; }
    
    public float Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public DateTime ProductionDate { get; set; }
}