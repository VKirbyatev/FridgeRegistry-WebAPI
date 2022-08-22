using AutoMapper;
using AutoMapper.QueryableExtensions;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, ICollection<CategoryLookupDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryListHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ICollection<CategoryLookupDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return categories;
    }
}