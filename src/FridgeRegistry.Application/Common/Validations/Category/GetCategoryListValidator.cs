using FluentValidation;
using FridgeRegistry.Application.Categories.Queries.GetCategoryList;

namespace FridgeRegistry.Application.Common.Validations.Category;

public class GetCategoryListValidator : AbstractValidator<GetCategoryListQuery>
{
    public GetCategoryListValidator()
    {
        Include(new PagingQueryValidator());
    }
}