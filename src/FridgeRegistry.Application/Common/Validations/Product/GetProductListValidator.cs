using FluentValidation;
using FridgeRegistry.Application.Products.Queries.GetProductsList;

namespace FridgeRegistry.Application.Common.Validations.Product;

public class GetProductListValidator : AbstractValidator<GetProductsListQuery>
{
    public GetProductListValidator()
    {
        Include(new PagingQueryValidator());
    }
}