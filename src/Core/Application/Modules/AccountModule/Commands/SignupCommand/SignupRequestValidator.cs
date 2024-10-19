using FluentValidation;
using Localization.Resources.Accounts;

namespace Application.Modules.AccountModule.Commands.SignupCommand
{
    class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(Account.NameCantBeEmpty)
                .NotNull().WithMessage(Account.NameCantBeEmpty)
                .MinimumLength(3).WithMessage(Account.NameMustLeastThreeSymbol);

            RuleFor(m => m.Surname).NotEmpty().WithMessage(Account.SurnameCantBeEmpty)
                .NotNull().WithMessage(Account.SurnameCantBeEmpty)
                .MinimumLength(3).WithMessage(Account.SurnameMustLeastThreeSymbol);

            RuleFor(m => m.Email).NotEmpty().WithMessage(Account.EmailCantBeEmpty)
                .EmailAddress().WithMessage(Account.EmailInvalid);

            RuleFor(m => m.Password).NotEmpty().WithMessage(Account.PasswordCantBeEmpty)
                .NotNull().WithMessage(Account.PasswordCantBeEmpty)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage(Account.PasswordInvalid);

            RuleFor(m => m.ConfirmPassword).NotEmpty().WithMessage(Account.PasswordCantBeEmpty)
                .NotNull().WithMessage(Account.PasswordCantBeEmpty)
                .Must((m, f) => f.Equals(m.Password)).WithMessage(Account.PasswordMustBeEqual);
        }
    }
}
