using FluentValidation;
using FridgeRegistry.Application.UserProducts.Commands.RemoveUserProduct;

namespace FridgeRegistry.Application.Common.Validations.UserProduct;

public class RemoveUserProductValidator : AbstractValidator<RemoveUserProductCommand>
{
    public RemoveUserProductValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}