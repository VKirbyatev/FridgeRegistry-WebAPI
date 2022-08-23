using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IDbContext _dbContext;

    public UpdateProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(
            x => x.Id == request.ProductId, cancellationToken
        );

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }
        
        product.SetName(request.Name);
        product.SetDescription(request.Description);
        product.SetShelfLife(request.ShelfLife);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}