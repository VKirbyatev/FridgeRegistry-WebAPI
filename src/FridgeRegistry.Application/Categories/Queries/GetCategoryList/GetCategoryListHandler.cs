using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.DTO.Common;
using FridgeRegistry.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, PagedListDto<CategoryLookupDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryListHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedListDto<CategoryLookupDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var searchString = request.SearchString ?? string.Empty;
        
        var categories = await _context.Categories
            .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
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