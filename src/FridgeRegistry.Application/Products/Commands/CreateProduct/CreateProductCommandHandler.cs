using FluentValidation;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Products;
using MediatR;

namespace FridgeRegistry.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IDbContext _dbContext;

    public CreateProductCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.ShelfLife
        );

        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    
        return product.Id;
    }
}