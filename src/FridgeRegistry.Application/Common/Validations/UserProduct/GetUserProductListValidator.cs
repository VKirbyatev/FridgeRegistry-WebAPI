using FluentValidation;
using FridgeRegistry.Application.Common.Query;
using FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;

namespace FridgeRegistry.Application.Common.Validations.UserProduct;

public class GetUserProductListValidator : AbstractValidator<GetUserProductListQuery>
{
    public GetUserProductListValidator()
    {
        Include(new PagingQueryValidator());

        When(query => query.SortType != null, () =>
        {
            RuleFor(query => query.SortBy)
                .NotEmpty()
                .WithMessage("Both SortType and SortBy params should be provided");
            RuleFor(query => query.SortType)
                .Must(sortType => sortType.ToUpper() == SortQueryParams.Asc || sortType.ToUpper() == SortQueryParams.Desc)
                .WithMessage("SortType should be equal to ASC or DESC");
        });
        
        When(query => query.SortBy != null, () =>
        {
            RuleFor(query => query.SortType)
                .NotEmpty()
                .WithMessage("Both SortType and SortBy params should be provided");
            RuleFor(query => query.SortBy)
                .Must(sortBy => SortQueryParams.UserProduct.AvailableSortMethods.Contains(sortBy.ToUpper()))
                .WithMessage($"SortBy should be equal one of this statements: {string.Join(", ", SortQueryParams.UserProduct.AvailableSortMethods)}");
        });
    }
}