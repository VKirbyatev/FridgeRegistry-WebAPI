using FridgeRegistry.WebAPI.Contracts.Requests.Common;

namespace FridgeRegistry.WebAPI.Contracts.Requests.UserProduct;

public class GetUserProductListRequest : PagingRequest
{
    public string? SortBy { get; set; }
    
    public string? SortType { get; set; }
}