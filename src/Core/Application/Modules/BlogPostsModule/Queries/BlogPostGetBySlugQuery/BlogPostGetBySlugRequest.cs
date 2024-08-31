using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetBySlugQuery
{
    public class BlogPostGetBySlugRequest : IRequest<BlogPostResponse>
    {
        public string Slug { get; set; }
    }
}
