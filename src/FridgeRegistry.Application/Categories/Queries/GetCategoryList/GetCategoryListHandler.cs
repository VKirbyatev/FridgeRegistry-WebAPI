using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, CategoryListDto>
{
    private readonly IDbContext _context;

    public GetCategoryListHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<CategoryListDto> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);
        var categoryList = new CategoryListDto()
        {
            Categories = new List<CategoryLookupDto>(),
        };

        foreach (var category in categories)
        {
            categoryList.Categories.Add(
                new CategoryLookupDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                }
                );
        }

        return categoryList;
    }
}