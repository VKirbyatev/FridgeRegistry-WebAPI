using FluentValidation;

namespace FridgeRegistry.Application.Products.Commands.RemoveProduct;

public class RemoveProductValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}