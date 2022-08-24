using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.UserProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.UserProducts.Commands.UpdateUserProduct;

public class UpdateUserProductCommandHandler : IRequestHandler<UpdateUserProductCommand>
{
    private readonly IDbContext _dbContext;

    public UpdateUserProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdateUserProductCommand request, CancellationToken cancellationToken)
    {
        var userProduct = await _dbContext.UserProducts.FirstOrDefaultAsync(
            userProduct => userProduct.Id == request.UserProductId, cancellationToken
        );

        if (userProduct == null)
        {
            throw new NotFoundException(nameof(UserProduct), request.UserProductId);
        }

        if (userProduct.UserId != request.UserId)
        {
            throw new NotOwnedException();
        }
        
        if (request.ProductionDate != null)
        {
            userProduct.SetProductionDate(request.ProductionDate ?? default);
        }

        if (request.Quantity != null)
        {
            userProduct.SetQuantity(request.Quantity ?? default);
        }
        
        if (request.QuantityType != null)
        {
            userProduct.SetQuantityType(request.QuantityType ?? default);
        }
        
        if (request.Commentary != null)
        {
            userProduct.SetCommentary(request.Commentary);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}