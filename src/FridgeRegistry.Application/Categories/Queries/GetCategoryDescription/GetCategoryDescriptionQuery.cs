using FridgeRegistry.Application.Contracts.Dto.Categories;
using MediatR;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;

public class GetCategoryDescriptionQuery : IRequest<CategoryDescriptionDto>
{
    public Guid CategoryId { get; set; }
}