using FluentValidation;

namespace Application.Modules.AccountModule.Commands.RefreshTokenCommand
{
    class RefreshTokenRequestValidator  : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.AccessToken).NotNull().WithMessage(x=>$"{nameof(x.AccessToken)} cant be null");
            RuleFor(x => x.RefreshToken).NotNull().WithMessage(x=>$"{nameof(x.RefreshToken)} cant be null");
        }
    }
}
