using FridgeRegistry.Application.DTO.Products;
using MediatR;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductsListQuery : IRequest<ICollection<ProductLookupDto>>
{
    
}