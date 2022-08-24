using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IDbContext _context;
    
    public UpdateCategoryCommandHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(
                category => category.Id == request.CategoryId && category.IsDeleted == false, cancellationToken
            );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        if (request.ParentCategoryId != null)
        {
            var parentCategory = await _context.Categories.FirstOrDefaultAsync(
                parent => parent.Id == request.ParentCategoryId && parent.IsDeleted == false, cancellationToken
            );

            if (parentCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.ParentCategoryId);
            }

            category.SetParent(parentCategory);
        }

        if (request.Name != null)
        {
            category.SetName(request.Name);    
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}