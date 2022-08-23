using AutoMapper;
using FridgeRegistry.Application.Common.Exceptions;
using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.Interfaces;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

public class GetCategoryProductsListQueryHandler : IRequestHandler<GetCategoryProductsListQuery, ICollection<ProductLookupDto>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper; 
    
    public GetCategoryProductsListQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ICollection<ProductLookupDto>> Handle(GetCategoryProductsListQuery request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.SingleOrDefaultAsync(
            category => category.Id == request.CategoryId, cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        var products = await GetCategoryProducts(category);

        return _mapper.Map<ICollection<ProductLookupDto>>(products);
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