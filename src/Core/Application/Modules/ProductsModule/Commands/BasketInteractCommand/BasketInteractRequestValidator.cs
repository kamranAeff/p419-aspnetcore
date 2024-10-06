using FluentValidation;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    class BasketInteractRequestValidator : AbstractValidator<BasketInteractRequest>
    {
        public BasketInteractRequestValidator()
        {
            RuleFor(m => m.ProductId).GreaterThan(0).WithMessage(m => $"{nameof(m.ProductId)} must be greater than zero");
            RuleFor(m => m.Count).GreaterThanOrEqualTo(0).WithMessage(m => $"{nameof(m.Count)} must be greater than or equal to zero");
        }
    }
}
