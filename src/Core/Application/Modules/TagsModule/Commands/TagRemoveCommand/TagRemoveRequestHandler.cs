using MediatR;
using Repositories;

namespace Application.Modules.TagsModule.Commands.TagRemoveCommand
{
    class TagRemoveRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagRemoveRequest>
    {
        public async Task Handle(TagRemoveRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            tagRepository.Remove(tag);
            await tagRepository.SaveAsync(cancellationToken);
        }
    }
}
