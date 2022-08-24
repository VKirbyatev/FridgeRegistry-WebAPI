using FluentValidation;

namespace FridgeRegistry.Application.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}