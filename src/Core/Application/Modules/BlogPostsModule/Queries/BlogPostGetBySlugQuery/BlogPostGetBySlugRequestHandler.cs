using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetBySlugQuery
{
    class BlogPostGetBySlugRequestHandler(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository,IHttpContextAccessor ctx) : IRequestHandler<BlogPostGetBySlugRequest, BlogPostResponse>
    {
        public async Task<BlogPostResponse> Handle(BlogPostGetBySlugRequest request, CancellationToken cancellationToken)
        {
            var query = from bp in blogPostRepository.GetAll(m => m.Slug.Equals(request.Slug))
                        join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                        select new BlogPostResponse
                        {
                            Id = bp.Id,
                            Title = bp.Title,
                            Slug = bp.Slug,
                            Body = bp.Body,
                            CategoryId = bp.CategoryId,
                            CategoryName = c.Name,
                            Image = $"{ctx.GetHost()}/files/{bp.ImagePath}",
                        };

            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException($"{typeof(BlogPost).Name} not found by expression");

            return entity;
        }
    }
}
