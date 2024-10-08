using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.SizesModule.Commands.SizeAddCommand
{
    class SizeAddRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizeAddRequest, Size>
    {
        public async Task<Size> Handle(SizeAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Size
            {
                Name = request.Name,
                SmallName = request.SmallName
            };

            await sizeRepository.AddAsync(entity, cancellationToken);
            await sizeRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
