using MediatR;

namespace Application.Modules.ContactPostsModule.Commands.ContactPostReplyCommand
{
    public class ContactPostReplyRequest : IRequest
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
