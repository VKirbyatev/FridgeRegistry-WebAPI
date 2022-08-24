using FluentValidation;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;

public class GetCategoryDescriptionValidator : AbstractValidator<GetCategoryDescriptionQuery>
{
    public GetCategoryDescriptionValidator()
    {
        RuleFor(query => query.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}