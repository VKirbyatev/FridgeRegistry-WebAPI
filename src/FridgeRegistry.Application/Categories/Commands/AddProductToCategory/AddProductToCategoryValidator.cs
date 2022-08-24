using FluentValidation;

namespace FridgeRegistry.Application.Categories.Commands.AddProductToCategory;

public class AddProductToCategoryValidator : AbstractValidator<AddProductToCategoryCommand>
{
    public AddProductToCategoryValidator()
    {
        RuleFor(query => query.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
        RuleFor(query => query.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}