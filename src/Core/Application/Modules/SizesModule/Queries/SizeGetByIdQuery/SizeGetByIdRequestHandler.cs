using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.SizesModule.Queries.SizeGetByIdQuery
{
    class SizeGetByIdRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizeGetByIdRequest, Size>
    {
        public async Task<Size> Handle(SizeGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeRepository.GetAsync(m=>m.Id == request.Id,cancellationToken);

            return entity;
        }
    }
}
