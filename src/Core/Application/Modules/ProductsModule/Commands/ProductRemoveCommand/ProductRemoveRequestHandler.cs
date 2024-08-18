using MediatR;
using Repositories;

namespace Application.Modules.ProductsModule.Commands.ProductRemoveCommand
{
    class ProductRemoveRequestHandler(IProductRepository productRepository) 
        : IRequestHandler<ProductRemoveRequest>
    {
        public async Task Handle(ProductRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            productRepository.Remove(entity);
            await productRepository.SaveAsync(cancellationToken);
        }
    }
}
