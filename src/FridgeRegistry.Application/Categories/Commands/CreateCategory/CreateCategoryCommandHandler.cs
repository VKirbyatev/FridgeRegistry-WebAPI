using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IDbContext _context;
    
    public CreateCategoryHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category(request.Name);
        
        await _context.Categories.AddAsync(newCategory, cancellationToken);

        if (request.ParentId != null)
        {
            var parentCategory = await _context.Categories
                .FirstOrDefaultAsync(
                    parent => parent.Id == request.ParentId && parent.IsDeleted == false, cancellationToken
                );

            if (parentCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.ParentId);
            }
            
            newCategory.SetParent(parentCategory);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return newCategory.Id;
    }
}