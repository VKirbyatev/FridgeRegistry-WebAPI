using FluentValidation;

namespace FridgeRegistry.Application.UserProducts.Commands.RemoveUserProduct;

public class RemoveUserProductValidator : AbstractValidator<RemoveUserProductCommand>
{
    public RemoveUserProductValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}