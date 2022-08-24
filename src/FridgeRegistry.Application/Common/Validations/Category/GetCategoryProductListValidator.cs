using FluentValidation;
using FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

namespace FridgeRegistry.Application.Common.Validations.Category;

public class GetCategoryProductListValidator : AbstractValidator<GetCategoryProductsListQuery>
{
    public GetCategoryProductListValidator()
    {
        Include(new PagingQueryValidator());
        RuleFor(query => query.CategoryId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}