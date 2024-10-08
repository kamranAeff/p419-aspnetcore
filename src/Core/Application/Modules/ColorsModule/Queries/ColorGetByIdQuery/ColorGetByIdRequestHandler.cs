using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ColorsModule.Queries.ColorGetByIdQuery
{
    class ColorGetByIdRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorGetByIdRequest, Color>
    {
        public async Task<Color> Handle(ColorGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await colorRepository.GetAsync(m=>m.Id == request.Id,cancellationToken);

            return entity;
        }
    }
}
