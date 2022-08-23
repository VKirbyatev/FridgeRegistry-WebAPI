using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, PagedListDto<ProductLookupDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductsListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PagedListDto<ProductLookupDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var searchString = request.SearchString ?? string.Empty;
        
        var products = await _dbContext.Products
            .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var totalProducts = _dbContext.Categories.Count(x => x.Name.ToLower().Contains(searchString.ToLower()));
        var totalPages = (totalProducts + request.Take - 1) / request.Take;

        return new PagedListDto<ProductLookupDto>()
        {
            TotalPages = totalPages,
            Items = products,
        };
    }
}