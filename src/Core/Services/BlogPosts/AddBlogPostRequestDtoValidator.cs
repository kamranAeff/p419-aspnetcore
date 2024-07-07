using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Services.BlogPosts
{
    class AddBlogPostRequestDtoValidator : AbstractValidator<AddBlogPostRequestDto>
    {
        public AddBlogPostRequestDtoValidator()
        {
            RuleFor(m => m.Image)
                .NotNull().WithMessage("Fayl bosh ola bilmez!")
                .Custom(CheckFile);

            RuleFor(m => m.Body)
                .NotEmpty().WithMessage("Metn bosh ola bilmez!")
                .NotNull().WithMessage("Metn bosh ola bilmez!")
                .MaximumLength(4000).WithMessage("Ən çox 4000 simvol daxil edə bilərsiz");

            //RuleFor(m => m.CategoryId)
            //    .GreaterThan(0).WithMessage("Kategoriya duzgun secilmeyib!");

            RuleFor(m => m.CategoryId)
                .GreaterThan(0).When(m => m.CategoryId is not null).WithMessage("Kategoriya duzgun secilmeyib!");
        }

        private void CheckFile(IFormFile file, ValidationContext<AddBlogPostRequestDto> context)
        {
            if (file?.ContentType.Contains("image/") == false)
                context.AddFailure(context.PropertyName, "Icaze verilen file novleri: (image/*)");

            if (file?.Length / Math.Pow(1024, 2) > 1)
            {
                context.AddFailure(context.PropertyName, "Fayl olcusu 5mb-den boyuk olmamalidir");
            }
        }
    }
}
