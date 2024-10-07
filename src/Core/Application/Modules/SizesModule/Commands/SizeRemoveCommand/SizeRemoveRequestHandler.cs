using MediatR;
using Repositories;

namespace Application.Modules.SizesModule.Commands.SizeRemoveCommand
{
    class SizeRemoveRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizeRemoveRequest>
    {
        public async Task Handle(SizeRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            sizeRepository.Remove(entity);
            await sizeRepository.SaveAsync(cancellationToken);
        }
    }
}
