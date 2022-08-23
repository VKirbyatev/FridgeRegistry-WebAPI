using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ICollection<ProductLookupDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductsListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ICollection<ProductLookupDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var products = await _dbContext.Products
            .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return products;
    }
}