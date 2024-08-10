using FluentValidation;

namespace Application.Modules.CategoriesModule.Commands.CategoryAddCommand
{
    class CategoryAddRequestValidator : AbstractValidator<CategoryAddRequest>
    {
        public CategoryAddRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name bosh buraxila bilmez")
                .NotNull().WithMessage("Name bosh buraxila bilmez")
                .MinimumLength(3).WithMessage("En az 3 simvol qeyd edilmelidir");
        }
    }
}
