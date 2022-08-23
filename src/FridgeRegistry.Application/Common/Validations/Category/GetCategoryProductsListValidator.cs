using FluentValidation;
using FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

namespace FridgeRegistry.Application.Common.Validations.Category;

public class GetCategoryProductsListValidator : AbstractValidator<GetCategoryProductsListQuery>
{
    public GetCategoryProductsListValidator()
    {
        RuleFor(query => query.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}