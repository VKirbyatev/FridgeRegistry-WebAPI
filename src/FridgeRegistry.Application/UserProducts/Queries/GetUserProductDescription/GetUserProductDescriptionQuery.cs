using FridgeRegistry.Application.DTO.UserProduct;
using MediatR;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductDescription;

public class GetUserProductDescriptionQuery : IRequest<UserProductDescriptionDto>
{
    public Guid UserProductId { get; set; }
    
    public string UserId { get; set; }
}