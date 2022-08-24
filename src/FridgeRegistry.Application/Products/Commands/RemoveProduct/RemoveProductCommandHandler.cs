using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Products.Commands.RemoveProduct;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
{
    private readonly IDbContext _dbContext;

    public RemoveProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(
                product => product.Id == request.ProductId && product.IsDeleted == false, cancellationToken
            );

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        product.Remove();
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}