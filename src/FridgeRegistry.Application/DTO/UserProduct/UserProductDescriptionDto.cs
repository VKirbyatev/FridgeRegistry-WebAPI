using FridgeRegistry.Domain.Common.Enums;

namespace FridgeRegistry.Application.DTO.UserProduct;

public class UserProductDescriptionDto
{
    public Guid Id { get; set; }
    
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }

    public float Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
    
    public string Commentary { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    public DateTime ProductionDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}