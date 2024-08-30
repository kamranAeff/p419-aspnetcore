using FluentValidation;

namespace Application.Modules.BrandsModule.Commands.BrandAddCommand
{
    class BrandAddRequestValidator : AbstractValidator<BrandAddRequest>
    {
        public BrandAddRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name bosh buraxila bilmez")
                .NotNull().WithMessage("Name bosh buraxila bilmez")
                .MinimumLength(3).WithMessage("En az 3 simvol qeyd edilmelidir");
        }
    }
}
