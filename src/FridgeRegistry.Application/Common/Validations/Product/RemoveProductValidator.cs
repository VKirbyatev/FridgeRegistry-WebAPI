using FluentValidation;
using FridgeRegistry.Application.Products.Commands.RemoveProduct;

namespace FridgeRegistry.Application.Common.Validations.Product;

public class RemoveProductValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}