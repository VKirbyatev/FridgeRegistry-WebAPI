using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.AddProductToCategory;

public class AddProductToCategoryCommandHandler : IRequestHandler<AddProductToCategoryCommand>
{
    private readonly IDbContext _dbContext;

    public AddProductToCategoryCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(
            category => category.Id == request.CategoryId && category.IsDeleted == false, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }
        
        var product = await _dbContext.Products.FirstOrDefaultAsync(
            product => product.Id == request.ProductId && product.IsDeleted == false, cancellationToken
        );
        
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        if (product.Category != null)
        {
            product.Category.RemoveProduct(product);
        }
        
        category.AddProduct(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}