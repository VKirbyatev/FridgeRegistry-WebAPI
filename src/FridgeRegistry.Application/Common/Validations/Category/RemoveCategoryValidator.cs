using FluentValidation;
using FridgeRegistry.Application.Categories.Commands.RemoveCategory;

namespace FridgeRegistry.Application.Common.Validations.Category;

public class RemoveCategoryValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryValidator()
    {
        RuleFor(command => command.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}