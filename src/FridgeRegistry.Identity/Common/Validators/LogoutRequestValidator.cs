using FluentValidation;
using FridgeRegistry.Identity.Contracts.Requests;

namespace FridgeRegistry.Identity.Common.Validators;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(request => request.AccessToken)
            .NotEmpty()
            .NotEqual(String.Empty)
            .NotNull();
        
        RuleFor(request => request.RefreshToken)
            .NotEqual(Guid.Empty)
            .NotEmpty()
            .NotNull();
    }
}