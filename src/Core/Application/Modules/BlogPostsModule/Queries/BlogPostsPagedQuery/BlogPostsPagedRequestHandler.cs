using Application.Common;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsPagedQuery
{
    class BlogPostsPagedRequestHandler(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository,IHttpContextAccessor ctx)
        : IRequestHandler<BlogPostsPagedRequest, IPagedResponse<BlogPostResponse>>
    {
        public async Task<IPagedResponse<BlogPostResponse>> Handle(BlogPostsPagedRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = from bp in blogPostRepository.GetAll()
                        join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                        select new BlogPostResponse
                        {
                            Id = bp.Id,
                            Title = bp.Title,
                            Slug = bp.Slug,
                            Body = bp.Body,
                            CategoryId = bp.CategoryId,
                            CategoryName = c.Name,
                            Image = $"{host}/files/{bp.ImagePath}",
                        };

            return await query.Sort(request).ToPaginateAsync(request, cancellationToken);
        }
    }
}
