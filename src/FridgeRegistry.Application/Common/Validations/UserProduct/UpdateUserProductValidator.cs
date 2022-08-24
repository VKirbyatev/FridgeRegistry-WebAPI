using FluentValidation;
using FridgeRegistry.Application.UserProducts.Commands.UpdateUserProduct;

namespace FridgeRegistry.Application.Common.Validations.UserProduct;

public class UpdateUserProductValidator : AbstractValidator<UpdateUserProductCommand>
{
    public UpdateUserProductValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}