using FridgeRegistry.Application.DTO.Products;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

public class GetCategoryProductsListQuery : IRequest<ICollection<ProductLookupDto>>
{
    public Guid CategoryId { get; set; }
}