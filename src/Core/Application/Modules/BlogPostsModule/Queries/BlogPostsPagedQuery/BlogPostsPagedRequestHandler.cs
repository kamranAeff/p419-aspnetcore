using Application.Common;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsPagedQuery
{
    class BlogPostsPagedRequestHandler(IBlogPostRepository blogPostRepository,IHttpContextAccessor ctx)
        : IRequestHandler<BlogPostsPagedRequest, IPagedResponse<BlogPostResponse>>
    {
        public async Task<IPagedResponse<BlogPostResponse>> Handle(BlogPostsPagedRequest request, CancellationToken cancellationToken)
        {
            var query = blogPostRepository.GetAll();


            var host = ctx.GetHost();

            return await query
                .Select(m => new BlogPostResponse
                {
                    Id = m.Id,
                    Body = m.Body,
                    CategoryId = m.CategoryId,
                    Image = $"{host}/files/{m.ImagePath}",
                })
                .Sort(request).ToPaginateAsync(request, cancellationToken);
        }
    }
}
