using FluentValidation;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    class BasketInteractRequestValidator : AbstractValidator<BasketInteractRequest>
    {
        public BasketInteractRequestValidator()
        {
            RuleFor(m => m.ProductCardId).NotEqual(Guid.Empty).WithMessage(m => $"{nameof(m.Count)} cant be empty");
            RuleFor(m => m.Count).GreaterThanOrEqualTo(0).WithMessage(m => $"{nameof(m.Count)} must be greater than or equal to zero");
        }
    }
}
