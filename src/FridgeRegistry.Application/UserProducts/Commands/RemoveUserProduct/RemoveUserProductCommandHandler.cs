using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.UserProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.UserProducts.Commands.RemoveUserProduct;

public class RemoveUserProductCommandHandler : IRequestHandler<RemoveUserProductCommand>
{
    private readonly IDbContext _dbContext;

    public RemoveUserProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(RemoveUserProductCommand request, CancellationToken cancellationToken)
    {
        var userProduct = await _dbContext.UserProducts
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

        _dbContext.UserProducts.Remove(userProduct);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}