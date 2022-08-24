using FluentValidation;

namespace FridgeRegistry.Application.UserProducts.Commands.UpdateUserProduct;

public class UpdateUserProductValidator : AbstractValidator<UpdateUserProductCommand>
{
    public UpdateUserProductValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}