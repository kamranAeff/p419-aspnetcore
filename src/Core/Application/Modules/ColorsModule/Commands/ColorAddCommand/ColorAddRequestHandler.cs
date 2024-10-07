using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ColorsModule.Commands.ColorAddCommand
{
    class ColorAddRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorAddRequest, Color>
    {
        public async Task<Color> Handle(ColorAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Color
            {
                Name = request.Name,
                HexCode=request.HexCode
            };

            await colorRepository.AddAsync(entity, cancellationToken);
            await colorRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
