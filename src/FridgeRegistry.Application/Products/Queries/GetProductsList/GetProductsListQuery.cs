using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.DTO.Products;
using MediatR;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductsListQuery : PagingQuery, IRequest<PagedListDto<ProductLookupDto>> {}