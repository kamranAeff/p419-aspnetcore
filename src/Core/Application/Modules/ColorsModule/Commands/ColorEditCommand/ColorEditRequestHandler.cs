using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ColorsModule.Commands.ColorEditCommand
{
    class ColorEditRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorEditRequest, Color>
    {
        public async Task<Color> Handle(ColorEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await colorRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
           
            entity.Name = request.Name;
            entity.HexCode = request.HexCode;

            await colorRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
