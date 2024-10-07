using FluentValidation;

namespace Application.Modules.SizesModule.Commands.SizeEditCommand
{
    class SizeEditRequestValidator : AbstractValidator<SizeEditRequest>
    {
        public SizeEditRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(m => $"{nameof(m.Name)} boş buraxıla bilməz")
                .NotNull().WithMessage(m => $"{nameof(m.Name)} boş buraxıla bilməz")
                .MinimumLength(3).WithMessage(m => $"{nameof(m.Name)} ən az 3 simvol qeyd edilməlidir");

            RuleFor(x => x.SmallName).NotEmpty().WithMessage(m => $"{nameof(m.SmallName)} boş buraxıla bilməz")
                .NotNull().WithMessage(m => $"{nameof(m.Name)} boş buraxıla bilməz");
        }
    }
}
