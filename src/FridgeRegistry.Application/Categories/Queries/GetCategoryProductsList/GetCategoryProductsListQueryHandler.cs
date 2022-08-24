using AutoMapper;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.Products;
using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

public class GetCategoryProductsListQueryHandler : IRequestHandler<GetCategoryProductsListQuery, PagedListDto<ProductLookupDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper; 
    
    public GetCategoryProductsListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedListDto<ProductLookupDto>> Handle(GetCategoryProductsListQuery request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.SingleOrDefaultAsync(
            category => category.Id == request.CategoryId && category.IsDeleted == false, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        var totalProducts = await GetCategoryProducts(category);
        
        var searchString = request.SearchString ?? string.Empty;
        var totalProductsCount = totalProducts.Count(x => x.Name.ToLower().Contains(searchString.ToLower()));
        var totalPages = (totalProductsCount + request.Take - 1) / request.Take;
        
        var pagedProducts = totalProducts
            .Where(x => x.Name.ToLower().Contains(searchString.ToLower()) && x.IsDeleted == false)
            .Skip(request.Skip)
            .Take(request.Take);
        

        return new PagedListDto<ProductLookupDto>()
        {
            TotalPages = totalPages,
            Items = _mapper.Map<ICollection<ProductLookupDto>>(pagedProducts)
        };
    }

    private async Task<IEnumerable<Product>> GetCategoryProducts(Category category)
    {
        var loadedCategory = await _dbContext.Categories
            .Include(x => x.Children)
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Id == category.Id);
        
        var products = loadedCategory?.Products.ToList();

        foreach (var child in category.Children)
        {
            var childProducts = await GetCategoryProducts(child);
            products?.AddRange(childProducts);
        }

        return products ?? new List<Product>();
    }
}