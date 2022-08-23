using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.DTO.Products;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

public class GetCategoryProductsListQuery : PagingQuery, IRequest<PagedListDto<ProductLookupDto>>
{
    public Guid CategoryId { get; set; }
}