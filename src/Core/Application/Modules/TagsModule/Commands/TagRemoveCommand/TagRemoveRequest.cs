using MediatR;

namespace Application.Modules.TagsModule.Commands.TagRemoveCommand
{
    public class TagRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
