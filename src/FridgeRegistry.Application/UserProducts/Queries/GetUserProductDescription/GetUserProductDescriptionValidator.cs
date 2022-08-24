using FluentValidation;

namespace FridgeRegistry.Application.UserProducts.Queries.GetUserProductDescription;

public class GetUserProductDescriptionValidator : AbstractValidator<GetUserProductDescriptionQuery>
{
    public GetUserProductDescriptionValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}