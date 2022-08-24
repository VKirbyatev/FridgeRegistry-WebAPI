using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.UserProducts.Commands.CreateUserProduct;

public class CreateUserProductCommandHandler : IRequestHandler<CreateUserProductCommand, Guid>
{
    private readonly IDbContext _dbContext;

    public CreateUserProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateUserProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(
            product => product.Id == request.ProductId && product.IsDeleted == false, cancellationToken
        );

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        var userProduct = new UserProduct(
            request.UserId,
            product,
            request.Quantity,
            request.QuantityType,
            request.ProductionDate ?? DateTime.UtcNow,
            request.Commentary
        );

        await _dbContext.UserProducts.AddAsync(userProduct, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userProduct.Id;
    }
}