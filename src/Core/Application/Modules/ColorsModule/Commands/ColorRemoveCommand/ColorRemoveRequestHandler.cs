using MediatR;
using Repositories;

namespace Application.Modules.ColorsModule.Commands.ColorRemoveCommand
{
    class ColorRemoveRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorRemoveRequest>
    {
        public async Task Handle(ColorRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await colorRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            colorRepository.Remove(entity);
            await colorRepository.SaveAsync(cancellationToken);
        }
    }
}
