using FluentValidation;
using FridgeRegistry.Application.Common.Constants.Sorting;
using FridgeRegistry.Application.Contracts.BaseClasses.Validators;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;

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
                .Must(sortType => sortType!.ToUpper() == SortTypes.Asc || sortType!.ToUpper() == SortTypes.Desc)
                .WithMessage("SortType should be equal to ASC or DESC");
        });
        
        When(query => query.SortBy != null, () =>
        {
            RuleFor(query => query.SortType)
                .NotEmpty()
                .WithMessage("Both SortType and SortBy params should be provided");
            RuleFor(query => query.SortBy)
                .Must(sortBy => UserProductsSortMethods.AvailableSortMethods.Contains(sortBy!.ToUpper()))
                .WithMessage($"SortBy should be equal one of this statements: {string.Join(", ", UserProductsSortMethods.AvailableSortMethods)}");
        });
    }
}