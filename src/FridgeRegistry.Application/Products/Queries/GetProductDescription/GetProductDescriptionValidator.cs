using FluentValidation;

namespace FridgeRegistry.Application.Products.Queries.GetProductDescription;

public class GetProductDescriptionValidator : AbstractValidator<GetProductDescriptionQuery>
{
    public GetProductDescriptionValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}