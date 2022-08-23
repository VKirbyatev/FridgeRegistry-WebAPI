using AutoMapper;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Products.Queries.GetProductDescription;

public class GetProductDescriptionQueryHandler : IRequestHandler<GetProductDescriptionQuery, ProductDescriptionDto>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductDescriptionQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductDescriptionDto> Handle(GetProductDescriptionQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Include(product => product.Category)
            .SingleOrDefaultAsync(
            x => x.Id == request.ProductId, cancellationToken
        );

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        return _mapper.Map<ProductDescriptionDto>(product);
    }
}