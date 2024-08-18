using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery
{
    class BlogPostGetByIdRequestHandler(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository,IHttpContextAccessor ctx) : IRequestHandler<BlogPostGetByIdRequest, BlogPostResponse>
    {
        public async Task<BlogPostResponse> Handle(BlogPostGetByIdRequest request, CancellationToken cancellationToken)
        {
            var query = from bp in blogPostRepository.GetAll(m => m.Id == request.Id)
                        join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                        select new BlogPostResponse
                        {
                            Id = bp.Id,
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
