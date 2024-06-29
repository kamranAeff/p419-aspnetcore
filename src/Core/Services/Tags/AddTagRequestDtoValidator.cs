using FluentValidation;

namespace Services.Tags
{
    class AddTagRequestDtoValidator : AbstractValidator<AddTagRequestDto>
    {
        public AddTagRequestDtoValidator()
        {
            RuleFor(m => m.Text)
                .NotNull()
                .WithMessage("Text bosh buraxila bilmez")
                .MinimumLength(3)
                .WithMessage("Text en az 3 simvoldan ibaret olmalidir");
        }
    }
}
