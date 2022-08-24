using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand>
{
    private readonly IDbContext _context;
    
    public RemoveCategoryCommandHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(
                category => category.Id == request.CategoryId && category.IsDeleted == false, cancellationToken
            );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        category.Remove();
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}