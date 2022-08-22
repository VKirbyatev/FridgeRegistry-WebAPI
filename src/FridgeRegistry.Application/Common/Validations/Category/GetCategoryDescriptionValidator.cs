using FluentValidation;
using FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;

namespace FridgeRegistry.Application.Common.Validations.Category;

public class GetCategoryDescriptionValidator : AbstractValidator<GetCategoryDescriptionQuery>
{
    public GetCategoryDescriptionValidator()
    {
        RuleFor(query => query.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}