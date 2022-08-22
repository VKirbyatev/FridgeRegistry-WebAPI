using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;

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
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}