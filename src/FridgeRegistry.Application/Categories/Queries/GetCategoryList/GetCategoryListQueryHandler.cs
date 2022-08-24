using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.Contracts.Dto.Categories;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, PagedListDto<CategoryLookupDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryListQueryHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedListDto<CategoryLookupDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var searchString = request.SearchString ?? string.Empty;
        
        var categories = await _context.Categories
            .Where(x => x.Name.ToLower().Contains(searchString.ToLower()) && x.IsDeleted == false)
            .Skip(request.Skip)
            .Take(request.Take)
            .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCategories = _context.Categories.Count(x => x.Name.ToLower().Contains(searchString.ToLower()));
        var totalPages = (totalCategories + request.Take - 1) / request.Take;

        return new PagedListDto<CategoryLookupDto>()
        {
            TotalPages = totalPages,
            Items = categories,
        };
    }
}