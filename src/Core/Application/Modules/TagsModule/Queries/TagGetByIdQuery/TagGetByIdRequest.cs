using Domain.Entities;
using MediatR;

namespace Application.Modules.TagsModule.Queries.TagGetByIdQuery
{
    public class TagGetByIdRequest : IRequest<Tag>
    {
        public int Id { get; set; }
    }
}
