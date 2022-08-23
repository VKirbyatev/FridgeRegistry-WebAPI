using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.DTO.Common;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : PagingQuery, IRequest<PagedListDto<CategoryLookupDto>> {}