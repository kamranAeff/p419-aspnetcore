using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.TagsModule.Commands.TagEditCommand
{
    class TagEditRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagEditRequest, Tag>
    {
        public async Task<Tag> Handle(TagEditRequest request, CancellationToken cancellationToken)
        {
            var tag = await tagRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            tag.Text = request.Text;
            await tagRepository.SaveAsync(cancellationToken);
            return tag;
        }
    }
}
