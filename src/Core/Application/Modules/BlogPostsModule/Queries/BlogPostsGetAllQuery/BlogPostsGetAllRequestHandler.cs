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

            var response = await blogPostRepository.GetAll()
                .Select(m => new BlogPostResponse
                {
                    Id = m.Id,
                    Body = m.Body,
                    CategoryId = m.CategoryId,
                    Image = $"{host}/files/{m.ImagePath}",
                })
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}
