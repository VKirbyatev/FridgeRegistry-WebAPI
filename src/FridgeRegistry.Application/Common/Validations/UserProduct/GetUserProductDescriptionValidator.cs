using FluentValidation;
using FridgeRegistry.Application.UserProducts.Queries.GetUserProductDescription;

namespace FridgeRegistry.Application.Common.Validations.UserProduct;

public class GetUserProductDescriptionValidator : AbstractValidator<GetUserProductDescriptionQuery>
{
    public GetUserProductDescriptionValidator()
    {
        RuleFor(query => query.UserProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}