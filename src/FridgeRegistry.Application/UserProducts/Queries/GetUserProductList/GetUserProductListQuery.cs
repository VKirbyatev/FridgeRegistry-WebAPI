using FridgeRegistry.Application.Contracts.BaseClasses.Query;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.UserProduct;
using MediatR;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;

public class GetUserProductListQuery : PagingQuery, IRequest<PagedListDto<UserProductLookupDto>>
{
    public string UserId { get; set; } = null!;
    
    public string? SortBy { get; set; }
    
    public string? SortType { get; set; }
}