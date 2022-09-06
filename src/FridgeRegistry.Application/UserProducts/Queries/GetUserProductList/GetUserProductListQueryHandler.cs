using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.Common.Constants.Sorting;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.UserProduct;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.UserProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;

public class GetUserProductListQueryHandler : IRequestHandler<GetUserProductListQuery, PagedListDto<UserProductLookupDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public GetUserProductListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PagedListDto<UserProductLookupDto>> Handle(GetUserProductListQuery request, CancellationToken cancellationToken)
    {
        var searchString = request.SearchString ?? string.Empty;

        var userProducts = _dbContext.UserProducts
            .Include(x => x.Product)
            .Where(
                x => x.Product.Name.ToLower().Contains(searchString.ToLower()) && x.UserId == request.UserId
            );

        userProducts = OrderUserProducts(userProducts, request.SortBy, request.SortType);
        var totalUserProducts = userProducts.Count();
            
        var result = await userProducts
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<UserProductLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        var totalPages = (totalUserProducts + request.Take - 1) / request.Take;
        var pageNumber = request.Skip / request.Take + 1;

        return new PagedListDto<UserProductLookupDto>()
        {
            Items = result,
            
            TotalPages = totalPages,
            PageNumber = pageNumber,
            
            PageSize = request.Take,
        };
    }

    private static IQueryable<UserProduct> OrderUserProducts(IQueryable<UserProduct> items, string? sortBy, string? sortType)
    {
        sortType = sortType?.ToUpper();
        sortBy = sortBy?.ToUpper();

        return sortBy switch
        {
            UserProductsSortMethods.ByName => sortType == SortTypes.Asc
                ? items.OrderBy(x => x.Product.Name)
                : items.OrderByDescending(x => x.Product.Name),
            
            UserProductsSortMethods.ByExpirationDate => sortType == SortTypes.Asc
                ? items.OrderBy(x => x.ExpirationDate)
                : items.OrderByDescending(x => x.ExpirationDate),
            
            _ => items.OrderByDescending(x => x.Product.CreatedAt)
        };
    }
}