using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsGetAllQuery
{
    public class BlogPostsGetAllRequest : IRequest<IEnumerable<BlogPostResponse>>
    {
    }
}
