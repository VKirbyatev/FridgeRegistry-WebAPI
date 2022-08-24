using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.DTO.UserProduct;
using FridgeRegistry.Application.Interfaces;
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
            .Where(x => x.Product.Name.ToLower().Contains(searchString.ToLower()) && x.UserId == request.UserId);

        userProducts = OrderUserProducts(userProducts, request.SortBy, request.SortType);    
            
        var result = await userProducts
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<UserProductLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalUserProducts = _dbContext.UserProducts.Count(
            x => x.Product.Name.ToLower().Contains(searchString.ToLower()) && x.UserId == request.UserId
        );
        var totalPages = (totalUserProducts + request.Take - 1) / request.Take;

        return new PagedListDto<UserProductLookupDto>()
        {
            TotalPages = totalPages,
            Items = result,
        };
    }

    private IQueryable<UserProduct> OrderUserProducts(IQueryable<UserProduct> items, string? sortBy, string? sortType)
    {
        sortType = sortType?.ToUpper();
        sortBy = sortBy?.ToUpper();

        return sortBy switch
        {
            SortQueryParams.UserProduct.ByName => sortType == SortQueryParams.Asc
                ? items.OrderBy(x => x.Product.Name)
                : items.OrderByDescending(x => x.Product.Name),
            
            SortQueryParams.UserProduct.ByExpirationDate => sortType == SortQueryParams.Asc
                ? items.OrderBy(x => x.ExpirationDate)
                : items.OrderByDescending(x => x.ExpirationDate),
            
            _ => items.OrderByDescending(x => x.Product.CreatedAt)
        };
    }
}