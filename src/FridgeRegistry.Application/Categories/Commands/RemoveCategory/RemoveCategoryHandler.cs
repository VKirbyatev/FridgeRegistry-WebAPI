using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryHandler : IRequestHandler<RemoveCategoryCommand>
{
    private readonly IDbContext _context;
    
    public RemoveCategoryHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(
            category => category.Id == request.CategoryId, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}