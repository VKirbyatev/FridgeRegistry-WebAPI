using FridgeRegistry.Application.Contracts.BaseClasses.Query;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.Products;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

public class GetCategoryProductsListQuery : PagingQuery, IRequest<PagedListDto<ProductLookupDto>>
{
    public Guid CategoryId { get; set; }
}