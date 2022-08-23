using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IDbContext _context;
    
    public UpdateCategoryHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(
            category => category.Id == request.CategoryId, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        if (request.ParentCategoryId != null)
        {
            var parentCategory = await _context.Categories.FirstOrDefaultAsync(
                parent => parent.Id == request.ParentCategoryId, cancellationToken
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