using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.BrandsModule.Commands.BrandAddCommand
{
    class BrandAddRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandAddRequest, Brand>
    {
        public async Task<Brand> Handle(BrandAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Brand
            {
                Name = request.Name
            };

            await brandRepository.AddAsync(entity, cancellationToken);
            await brandRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
