using FluentValidation;
using Localization.Resources.ContactPosts;

namespace Application.Modules.ContactPostsModule.Commands.ContactPostApplyCommand
{
    class ContactPostApplyRequestValidator : AbstractValidator<ContactPostApplyRequest>
    {
        public ContactPostApplyRequestValidator()
        {
            RuleFor(m => m.FullName).NotNull().WithMessage(m =>ContactPost.FullNameCantBeNull)
                .NotEmpty().WithMessage(m => ContactPost.FullNameCantBeEmpty);

            RuleFor(m => m.Email).NotNull().WithMessage(m=> $"{nameof(m.Email)} cant be null")
                .NotEmpty().WithMessage(m => $"{nameof(m.Email)} cant be empty")
                .EmailAddress().WithMessage(m => $"{nameof(m.Email)} is invalid email address");

            RuleFor(m => m.Message).NotNull().WithMessage(m=> $"{nameof(m.Message)} cant be null")
                .NotEmpty().WithMessage(m => $"{nameof(m.Message)} cant be empty");
        }
    }
}
