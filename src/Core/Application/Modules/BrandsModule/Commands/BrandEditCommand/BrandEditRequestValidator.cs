using FluentValidation;

namespace Application.Modules.BrandsModule.Commands.BrandEditCommand
{
    class BrandEditRequestValidator : AbstractValidator<BrandEditRequest>
    {
        public BrandEditRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name bosh buraxila bilmez")
               .NotNull().WithMessage("Name bosh buraxila bilmez");
        }
    }
}
