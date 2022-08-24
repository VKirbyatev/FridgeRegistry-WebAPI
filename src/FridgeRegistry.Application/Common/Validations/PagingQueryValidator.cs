using FluentValidation;
using FridgeRegistry.Application.Common.Query;

namespace FridgeRegistry.Application.Common.Validations;

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