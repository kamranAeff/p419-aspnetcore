using FluentValidation;

namespace Application.Modules.ColorsModule.Commands.ColorEditCommand
{
    class ColorEditRequestValidator : AbstractValidator<ColorEditRequest>
    {
        public ColorEditRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(m => $"{nameof(m.Name)} boş buraxıla bilməz")
                .NotNull().WithMessage(m => $"{nameof(m.Name)} boş buraxıla bilməz")
                .MinimumLength(3).WithMessage(m => $"{nameof(m.Name)} ən az 3 simvol qeyd edilməlidir");

            RuleFor(x => x.HexCode)
                .NotEmpty().WithMessage(m => $"{nameof(m.HexCode)} boş buraxıla bilməz")
                .Matches(@"^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6})(?:[0-9a-fA-F]{2})?$").WithMessage("Düzgün rəng kodu daxil edin. nümunə: #ffffff, #fff və ya #ffffffaa");
        }
    }
}
