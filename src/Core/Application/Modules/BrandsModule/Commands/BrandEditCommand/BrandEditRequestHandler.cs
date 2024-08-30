using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.BrandsModule.Commands.BrandEditCommand
{
    class BrandEditRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandEditRequest, Brand>
    {
        public async Task<Brand> Handle(BrandEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await brandRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
           
            entity.Name = request.Name;

            await brandRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
