using AutoMapper;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.Domain.Categories;

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
    }
}