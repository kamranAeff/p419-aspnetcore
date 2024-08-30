using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.BrandsModule.Queries.BrandGetByIdQuery
{
    class BrandGetByIdRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandGetByIdRequest, Brand>
    {
        public async Task<Brand> Handle(BrandGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await brandRepository.GetAsync(m=>m.Id == request.Id,cancellationToken);

            return entity;
        }
    }
}
