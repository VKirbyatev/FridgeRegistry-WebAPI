using FluentValidation;
using FridgeRegistry.Application.Products.Commands.UpdateProduct;

namespace FridgeRegistry.Application.Common.Validations.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}