using Domain.Entities;
using MediatR;

namespace Application.Modules.TagsModule.Commands.TagAddCommand
{
    public class TagAddRequest : IRequest<Tag>
    {
        public string? Text { get; set; }
    }
}
