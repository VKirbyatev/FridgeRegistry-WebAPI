using FluentValidation;

namespace FridgeRegistry.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(command => command.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
        
        RuleFor(command => command.ParentCategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}