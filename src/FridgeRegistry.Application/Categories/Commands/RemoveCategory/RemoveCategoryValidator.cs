using FluentValidation;

namespace FridgeRegistry.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryValidator()
    {
        RuleFor(command => command.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}