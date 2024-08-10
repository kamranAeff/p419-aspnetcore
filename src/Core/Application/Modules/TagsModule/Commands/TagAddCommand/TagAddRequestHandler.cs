using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.TagsModule.Commands.TagAddCommand
{
    class TagAddRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagAddRequest, Tag>
    {
        public async Task<Tag> Handle(TagAddRequest request, CancellationToken cancellationToken)
        {
            var tag = new Tag { Text = request.Text };
            await tagRepository.AddAsync(tag, cancellationToken);
            await tagRepository.SaveAsync(cancellationToken);
            return tag;
        }
    }
}
