using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Modules.TagsModule.Queries.TagsPagedQuery
{
    public class TagsPagedRequest : Pageable,IRequest<IPagedResponse<Tag>>
    {
    }
}
