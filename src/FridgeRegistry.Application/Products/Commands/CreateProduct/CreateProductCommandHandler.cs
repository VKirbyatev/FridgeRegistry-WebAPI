using FluentValidation;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        if (request.CategoryId != null)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(
                x => x.Id == request.CategoryId, cancellationToken
            );

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }
            
            category.AddProduct(product);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    
        return product.Id;
    }
}