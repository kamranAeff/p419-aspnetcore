using Application.Common;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.TagsModule.Queries.TagsPagedQuery
{
    class TagsPagedRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagsPagedRequest, IPagedResponse<Tag>>
    {
        public async Task<IPagedResponse<Tag>> Handle(TagsPagedRequest request, CancellationToken cancellationToken)
        {
            var response = await tagRepository.GetAll()
                .ToPaginateAsync(request, cancellationToken);

            return response;
        }
    }
}
