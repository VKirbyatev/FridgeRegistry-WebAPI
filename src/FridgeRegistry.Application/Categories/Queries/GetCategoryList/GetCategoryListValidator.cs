using FluentValidation;
using FridgeRegistry.Application.Contracts.BaseClasses.Validators;

namespace FridgeRegistry.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListValidator : AbstractValidator<GetCategoryListQuery>
{
    public GetCategoryListValidator()
    {
        Include(new PagingQueryValidator());
    }
}