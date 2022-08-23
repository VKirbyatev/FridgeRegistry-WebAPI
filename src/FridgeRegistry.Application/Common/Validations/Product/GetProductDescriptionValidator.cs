using FluentValidation;
using FridgeRegistry.Application.Products.Queries.GetProductDescription;

namespace FridgeRegistry.Application.Common.Validations.Product;

public class GetProductDescriptionValidator : AbstractValidator<GetProductDescriptionQuery>
{
    public GetProductDescriptionValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}