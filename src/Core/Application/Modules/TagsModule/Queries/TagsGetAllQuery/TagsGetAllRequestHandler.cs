using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.TagsModule.Queries.TagsGetAllQuery
{
    class TagsGetAllRequestHandler(ITagRepository tagRepository) : IRequestHandler<TagsGetAllRequest, IEnumerable<Tag>>
    {
        public async Task<IEnumerable<Tag>> Handle(TagsGetAllRequest request, CancellationToken cancellationToken)
        {
            var response = await tagRepository.GetAll().ToListAsync(cancellationToken);
            return response;
        }
    }
}
