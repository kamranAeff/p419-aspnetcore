using FluentValidation;

namespace Application.Modules.TagsModule.Commands.TagAddCommand
{
    class TagAddRequestValidator : AbstractValidator<TagAddRequest>
    {
        public TagAddRequestValidator()
        {
            RuleFor(m=>m.Text).NotEmpty().WithMessage(m=>$"{nameof(m.Text)} cant be null");
        }
    }
}
