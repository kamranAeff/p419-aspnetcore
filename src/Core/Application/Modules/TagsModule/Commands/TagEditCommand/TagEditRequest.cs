using Domain.Entities;
using MediatR;

namespace Application.Modules.TagsModule.Commands.TagEditCommand
{
    public class TagEditRequest : IRequest<Tag>
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
