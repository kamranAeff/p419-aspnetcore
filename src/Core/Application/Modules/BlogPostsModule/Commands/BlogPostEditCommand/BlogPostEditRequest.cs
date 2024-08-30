using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostEditCommand
{
    public class BlogPostEditRequest : IRequest<BlogPostResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
