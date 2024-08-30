using MediatR;
using Repositories;

namespace Application.Modules.BrandsModule.Commands.BrandRemoveCommand
{
    class BrandRemoveRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandRemoveRequest>
    {
        public async Task Handle(BrandRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await brandRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            brandRepository.Remove(entity);
            await brandRepository.SaveAsync(cancellationToken);
        }
    }
}
