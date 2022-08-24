using AutoMapper;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Dto.Categories;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;

public class GetCategoryDescriptionQueryHandler : IRequestHandler<GetCategoryDescriptionQuery, CategoryDescriptionDto>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryDescriptionQueryHandler(IDbContext context, IMapper mapper)
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
                category => category.Id == request.CategoryId && category.IsDeleted == false, cancellationToken
            );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }
        
        return _mapper.Map<CategoryDescriptionDto>(category);
    }
}