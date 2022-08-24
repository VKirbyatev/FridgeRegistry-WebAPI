using AutoMapper;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Dto.UserProduct;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.UserProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductDescription;

public class GetUserProductDescriptionQueryHandler : IRequestHandler<GetUserProductDescriptionQuery, UserProductDescriptionDto>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public GetUserProductDescriptionQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UserProductDescriptionDto> Handle(GetUserProductDescriptionQuery request, CancellationToken cancellationToken)
    {
        var userProduct = await _dbContext.UserProducts
            .Include(userProduct => userProduct.Product)
            .FirstOrDefaultAsync(
                userProduct => userProduct.Id == request.UserProductId, cancellationToken
            );

        if (userProduct == null)
        {
            throw new NotFoundException(nameof(UserProduct), request.UserProductId);
        }

        if (userProduct.UserId != request.UserId)
        {
            throw new ForbiddenResourceException();
        }

        return _mapper.Map<UserProductDescriptionDto>(userProduct);
    }
}