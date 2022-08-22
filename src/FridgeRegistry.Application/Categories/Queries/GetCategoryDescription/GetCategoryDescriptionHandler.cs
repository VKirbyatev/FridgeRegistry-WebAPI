using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IMapper _mapper;

    public GetCategoryDescriptionHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
        
        return _mapper.Map<CategoryDescriptionDto>(category);
    }
}