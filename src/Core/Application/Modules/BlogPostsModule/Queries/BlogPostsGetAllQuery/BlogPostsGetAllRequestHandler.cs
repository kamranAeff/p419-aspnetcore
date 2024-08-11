using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsGetAllQuery
{
    class BlogPostsGetAllRequestHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor ctx) : IRequestHandler<BlogPostsGetAllRequest, IEnumerable<BlogPostResponse>>
    {
        public async Task<IEnumerable<BlogPostResponse>> Handle(BlogPostsGetAllRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = blogPostRepository.GetAll();

            #region Filter
            if (!string.IsNullOrWhiteSpace(request.Body))
            {
                query = query.Where(m => m.Body.Contains(request.Body));
            }

            if (request.Category is not null && request.Category.Count() > 0)
            {
                query = query.Where(m => request.Category.Contains(m.CategoryId));
            }
            #endregion

            var response = await query.Select(m => new BlogPostResponse
            {
                Id = m.Id,
                Body = m.Body,
                CategoryId = m.CategoryId,
                Image = $"{host}/files/{m.ImagePath}",
            })
                .Sort(request)
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}
