using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery
{
    public class BlogPostGetByIdRequest : IRequest<BlogPostResponse>
    {
        public int Id { get; set; }
    }
}
