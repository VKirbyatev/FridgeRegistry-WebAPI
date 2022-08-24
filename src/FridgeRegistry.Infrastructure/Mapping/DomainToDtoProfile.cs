using AutoMapper;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.DTO.UserProduct;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts;

namespace FridgeRegistry.Infrastructure.Mapping;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Category, CategoryDescriptionDto>()
            .ForMember(dest => dest.ParentId,
                options => options.MapFrom(
                    src => src.Parent != null ? src.Parent.Id : Guid.Empty
                ))
            .ForMember(dest => dest.Children,
                options => options.MapFrom(
                    (src, dest, lookup, context) 
                        => context.Mapper.Map<IEnumerable<Category>, ICollection<CategoryLookupDto>>(src.Children)
                ));

        CreateMap<Category, CategoryLookupDto>();

        CreateMap<Product, ProductDescriptionDto>()
            .ForMember(dest => dest.CategoryId,
                options => options.MapFrom(
                    src => src.Category != null ? src.Category.Id : Guid.Empty
                ))
            .ForMember(dest => dest.CategoryName,
                options => options.MapFrom(
                    src => src.Category != null ? src.Category.Name : string.Empty
                ))
            .ForMember(dest => dest.ShelfLife,
                options => options.MapFrom(
                    src => src.ShelfLife.ToString()
                ));

        CreateMap<Product, ProductLookupDto>()
            .ForMember(dest => dest.ShelfLife,
                options => options.MapFrom(
                    src => src.ShelfLife.ToString()
                ));

        CreateMap<UserProduct, UserProductDescriptionDto>()
            .ForMember(dest => dest.ProductId,
                options => options.MapFrom(
                    src => src.Product.Id
                ))
            .ForMember(dest => dest.ProductName,
                options => options.MapFrom(
                    src => src.Product.Name
                ));
        
        CreateMap<UserProduct, UserProductLookupDto>()
            .ForMember(dest => dest.ProductName,
                options => options.MapFrom(
                    src => src.Product.Name
                ));
    }
}