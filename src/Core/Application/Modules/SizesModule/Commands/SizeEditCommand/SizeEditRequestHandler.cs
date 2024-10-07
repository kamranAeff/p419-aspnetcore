using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.SizesModule.Commands.SizeEditCommand
{
    class SizeEditRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizeEditRequest, Size>
    {
        public async Task<Size> Handle(SizeEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
           
            entity.Name = request.Name;
            entity.SmallName = request.SmallName;

            await sizeRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
