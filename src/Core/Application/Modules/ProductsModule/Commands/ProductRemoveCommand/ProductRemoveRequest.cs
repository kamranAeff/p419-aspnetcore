using MediatR;

namespace Application.Modules.ProductsModule.Commands.ProductRemoveCommand
{
    public class ProductRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
