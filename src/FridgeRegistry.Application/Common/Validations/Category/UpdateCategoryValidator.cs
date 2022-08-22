using FluentValidation;
using FridgeRegistry.Application.Categories.Commands.UpdateCategory;

namespace FridgeRegistry.Application.Common.Validations.Category;

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