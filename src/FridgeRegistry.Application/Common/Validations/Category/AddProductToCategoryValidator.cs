using FluentValidation;
using FridgeRegistry.Application.Categories.Commands.AddProductToCategory;

namespace FridgeRegistry.Application.Common.Validations.Category;

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