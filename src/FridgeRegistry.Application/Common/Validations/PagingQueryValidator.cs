using FluentValidation;
using FridgeRegistry.Application.Common.Query;

namespace FridgeRegistry.Application.Common.Validations;

public class PagingQueryValidator: AbstractValidator<PagingQuery>
{
    public PagingQueryValidator()
    {
        RuleFor(query => query.Take)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
        
        RuleFor(query => query.Skip)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}