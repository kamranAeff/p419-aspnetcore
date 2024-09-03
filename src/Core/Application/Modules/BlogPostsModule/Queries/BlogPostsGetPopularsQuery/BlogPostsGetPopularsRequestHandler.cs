using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostsGetPopularsQuery
{
    class BlogPostsGetPopularsRequestHandler(IBlogPostRepository blogPostResponse,IHttpContextAccessor ctx) : IRequestHandler<BlogPostsGetPopularsRequest, IEnumerable<BlogPostResponse>>
    {
        public async Task<IEnumerable<BlogPostResponse>> Handle(BlogPostsGetPopularsRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var response = await blogPostResponse.GetAll()
                                              .OrderByDescending(m => m.Id)
                                              .Take(4)
                                              .Select(m => new BlogPostResponse
                                              {
                                                  Id = m.Id,
                                                  Title = m.Title,
                                                  Slug = m.Slug,
                                                  Body = m.Body,
                                                  CategoryId = m.CategoryId,
                                                  Image = $"{host}/files/{m.ImagePath}",
                                              })
                                              .ToListAsync(cancellationToken);

            return response;
        }
    }
}
