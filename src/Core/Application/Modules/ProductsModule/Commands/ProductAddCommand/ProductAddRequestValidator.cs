using FluentValidation;

namespace Application.Modules.ProductsModule.Commands.ProductAddCommand
{
    class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            RuleFor(m => m.Title).NotEmpty().WithMessage("Title bosh buraxila bilmez")
            .NotNull().WithMessage("Title bosh buraxila bilmez")
            .MinimumLength(2).WithMessage("En az 2 simvol qeyd edilmelidir");

            RuleFor(m => m.BrandId)
            .GreaterThanOrEqualTo(1).WithMessage("Marka secilmeyib");

            RuleFor(m => m.CategoryId)
            .GreaterThanOrEqualTo(1).WithMessage("Kategoriya secilmeyib");

            RuleFor(m => m.UnitOfWeight)
            .IsInEnum().WithMessage("Olcu vahidi secilmeyib");

            RuleFor(m => m.Description).NotEmpty().WithMessage("Description bosh buraxila bilmez")
            .NotNull().WithMessage("Description bosh buraxila bilmez");

            RuleFor(m => m.Information).NotEmpty().WithMessage("Information bosh buraxila bilmez")
            .NotNull().WithMessage("Information bosh buraxila bilmez");

            //RuleFor(m => m.Images).NotEmpty().WithMessage("Sekil secilmeyib")
            //.NotNull().WithMessage("Sekil secilmeyib")
            //.Must((model, field) => field is not null && field.Any(m => m.IsMain)).WithMessage("Eses shekil secilmeyib");

            //RuleFor(m => m.Cards).NotEmpty().WithMessage("Çeşidlənməyib")
            //.NotNull().WithMessage("Çeşidlənməyib")
            //.Must((model, field) => field is not null && field.Any(m => m.IsDefault)).WithMessage("Əsas çeşid seçilmıyib");
        }
    }
}
