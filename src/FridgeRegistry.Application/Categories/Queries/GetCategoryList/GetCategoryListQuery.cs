using FridgeRegistry.Application.Contracts.BaseClasses.Query;
using FridgeRegistry.Application.Contracts.Dto.Categories;
using FridgeRegistry.Application.Contracts.Dto.Common;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : PagingQuery, IRequest<PagedListDto<CategoryLookupDto>> {}