using Domain.Entities;
using MediatR;

namespace Application.Modules.TagsModule.Queries.TagsGetAllQuery
{
    public class TagsGetAllRequest : IRequest<IEnumerable<Tag>>
    {
    }
}
