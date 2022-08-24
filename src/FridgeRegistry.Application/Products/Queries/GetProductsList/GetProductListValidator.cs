using FluentValidation;
using FridgeRegistry.Application.Contracts.BaseClasses.Validators;

namespace FridgeRegistry.Application.Products.Queries.GetProductsList;

public class GetProductListValidator : AbstractValidator<GetProductsListQuery>
{
    public GetProductListValidator()
    {
        Include(new PagingQueryValidator());
    }
}