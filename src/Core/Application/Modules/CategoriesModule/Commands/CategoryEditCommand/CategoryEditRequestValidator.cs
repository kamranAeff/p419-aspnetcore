using FluentValidation;

namespace Application.Modules.CategoriesModule.Commands.CategoryEditCommand
{
    class CategoryEditRequestValidator : AbstractValidator<CategoryEditRequest>
    {
        public CategoryEditRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name bosh buraxila bilmez")
               .NotNull().WithMessage("Name bosh buraxila bilmez");
        }
    }
}
