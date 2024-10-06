using Domain.Entities;
using MediatR;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    public class BasketInteractRequest : IRequest<Basket>
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
