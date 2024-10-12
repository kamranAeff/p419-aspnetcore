using Domain.Entities;
using MediatR;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    public class BasketInteractRequest : IRequest<BasketResponse>
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
}
