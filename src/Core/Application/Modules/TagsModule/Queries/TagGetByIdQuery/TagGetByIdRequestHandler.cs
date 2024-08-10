using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.TagsModule.Queries.TagGetByIdQuery
{
    class TagGetByIdRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagGetByIdRequest, Tag>
    {
        public async Task<Tag> Handle(TagGetByIdRequest request, CancellationToken cancellationToken)
        {
            var response = await tagRepository.GetAsync(m=>m.Id == request.Id,cancellationToken);
            return response;
        }
    }
}
