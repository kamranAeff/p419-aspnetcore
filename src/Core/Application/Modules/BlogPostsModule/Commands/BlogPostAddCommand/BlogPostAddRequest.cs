using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostAddCommand
{
    public class BlogPostAddRequest : IRequest<BlogPostResponse>
    {
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
