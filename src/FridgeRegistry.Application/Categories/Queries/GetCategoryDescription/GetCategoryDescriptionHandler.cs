using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;

public class GetCategoryDescriptionHandler : IRequestHandler<GetCategoryDescriptionQuery, CategoryDescriptionDto>
{
    private readonly IDbContext _context;

    public GetCategoryDescriptionHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<CategoryDescriptionDto> Handle(GetCategoryDescriptionQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(category => category.Parent)
            .Include(category => category.Children)
            .SingleOrDefaultAsync(
            category => category.Id == request.CategoryId, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        var categoryDescription = new CategoryDescriptionDto()
        {
            Id = category.Id,
            ParentId = category.Parent?.Id,
            
            Name = category.Name,
            Children = new List<CategoryLookupDto>(),
            
            CreatedAt = category.CreatedAt,
            ModifiedAt = category.ModifiedAt,
        };

        foreach (var child in category.Children)
        {
            categoryDescription.Children.Add(
                new CategoryLookupDto()
                {
                    Id = child.Id,
                    Name = child.Name,
                }
                );
        }

        return categoryDescription;
    }
}