using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.DTO.UserProduct;
using MediatR;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;

public class GetUserProductListQuery : PagingQuery, IRequest<PagedListDto<UserProductLookupDto>>
{
    public string UserId { get; set; }
    
    public string? SortBy { get; set; }
    
    public string? SortType { get; set; }
}