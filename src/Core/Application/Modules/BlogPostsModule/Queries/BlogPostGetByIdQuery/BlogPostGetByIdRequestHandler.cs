using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery
{
    class BlogPostGetByIdRequestHandler(IBlogPostRepository blogPostRepository,IHttpContextAccessor ctx) : IRequestHandler<BlogPostGetByIdRequest, BlogPostResponse>
    {
        public async Task<BlogPostResponse> Handle(BlogPostGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await blogPostRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            return new BlogPostResponse
            {
                Id = entity.Id,
                Body = entity.Body,
                CategoryId = entity.CategoryId,
                Image = $"{ctx.GetHost()}/files/{entity.ImagePath}",
            };
        }
    }
}
