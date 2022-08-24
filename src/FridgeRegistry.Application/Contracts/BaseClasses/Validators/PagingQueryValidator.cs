using FluentValidation;
using FridgeRegistry.Application.Contracts.BaseClasses.Query;

namespace FridgeRegistry.Application.Contracts.BaseClasses.Validators;

public class PagingQueryValidator: AbstractValidator<PagingQuery>
{
    public PagingQueryValidator()
    {
        RuleFor(query => query.SearchString)
            .MaximumLength(100);
        
        RuleFor(query => query.Take)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(query => query.Skip)
            .GreaterThanOrEqualTo(0);
    }
}