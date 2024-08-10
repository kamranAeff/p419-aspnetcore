using MediatR;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostRemoveCommand
{
    public class BlogPostRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
