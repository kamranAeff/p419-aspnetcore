using FluentValidation;

namespace Application.Modules.AccountModule.Commands.SignupCommand
{
    class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(m => m.UserName).NotEmpty().WithMessage("UserName bosh buraxila bilmez")
                .NotNull().WithMessage("UserName bosh buraxila bilmez")
                .MinimumLength(3).WithMessage("UserName en az 3 simvol qeyd edilmelidir");

            RuleFor(m => m.Email).NotEmpty().WithMessage("Email bosh buraxila bilmez")
                .EmailAddress().WithMessage("Email formati uygun deyil");

            RuleFor(m => m.Password).NotEmpty().WithMessage("Password bosh buraxila bilmez")
                .NotNull().WithMessage("Password bosh buraxila bilmez")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Password must be at least 8 characters long, with at least one uppercase letter, one lowercase letter, one digit, and one special character."); ;

            RuleFor(m => m.ConfirmPassword).NotEmpty().WithMessage("Password bosh buraxila bilmez")
                .NotNull().WithMessage("Password bosh buraxila bilmez")
                .Must((m, f) => f.Equals(m.Password)).WithMessage("Password eynilik teskil etmir");
        }
    }
}
