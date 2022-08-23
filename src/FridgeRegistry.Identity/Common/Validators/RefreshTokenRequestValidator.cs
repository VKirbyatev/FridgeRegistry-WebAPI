using FluentValidation;
using FridgeRegistry.Identity.Contracts.Requests;

namespace FridgeRegistry.Identity.Common.Validators;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(request => request.AccessToken)
            .NotEqual(String.Empty)
            .NotEmpty()
            .NotNull();
        
        RuleFor(request => request.RefreshToken)
            .NotEqual(Guid.Empty)
            .NotEmpty()
            .NotNull();
    }
}