using Application.Common;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsGetAllQuery
{
    public class BlogPostsGetAllRequest : IRequest<IEnumerable<BlogPostResponse>>,ISortable
    {
        public string Body { get; set; }
        public IEnumerable<int> Category { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
