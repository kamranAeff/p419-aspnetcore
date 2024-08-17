using Application.Common;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsPagedQuery
{
    public class BlogPostsPagedRequest : Pageable, IRequest<IPagedResponse<BlogPostResponse>>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
