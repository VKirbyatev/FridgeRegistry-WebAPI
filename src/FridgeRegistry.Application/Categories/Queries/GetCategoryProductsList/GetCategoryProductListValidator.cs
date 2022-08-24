using FluentValidation;
using FridgeRegistry.Application.Contracts.BaseClasses.Validators;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;

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