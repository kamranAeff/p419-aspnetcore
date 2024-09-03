using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsGetPopularsQuery
{
    public class BlogPostsGetPopularsRequest : IRequest<IEnumerable<BlogPostResponse>>
    {
        public int RecordCount { get; set; }
    }
}
