using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
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
        var category = new Category(request.Name);
        
        await _context.Categories.AddAsync(category, cancellationToken);

        if (request.ParentId != null)
        {
            var parentCategory = await _context.Categories.SingleOrDefaultAsync(
                x => x.Id == request.ParentId, cancellationToken
            );

            if (parentCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.ParentId);
            }
            
            category.SetParent(parentCategory);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}