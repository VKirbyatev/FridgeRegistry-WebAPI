using FridgeRegistry.Application.Contracts.BaseClasses.Query;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.Products;
using MediatR;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductsListQuery : PagingQuery, IRequest<PagedListDto<ProductLookupDto>> {}