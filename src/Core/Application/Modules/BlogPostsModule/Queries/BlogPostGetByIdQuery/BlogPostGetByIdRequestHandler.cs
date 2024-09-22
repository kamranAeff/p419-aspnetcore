using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;

namespace Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery
{
    class BlogPostGetByIdRequestHandler(IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository,
        IHttpContextAccessor ctx,
        ILogger<BlogPostGetByIdRequestHandler> logger) : IRequestHandler<BlogPostGetByIdRequest, BlogPostResponse>
    {
        //RDBMS
        //NoSQL
        public async Task<BlogPostResponse> Handle(BlogPostGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("build query");
            var query = from bp in blogPostRepository.GetAll(m => m.Id == request.Id)
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

            logger.LogDebug("query builded");
            logger.LogDebug("get entity");
            var entity = await query.FirstOrDefaultAsync(cancellationToken);
            logger.LogDebug("entity already in ram");

            logger.LogDebug("check entity is null or not");
            if (entity is null)
                throw new NotFoundException($"{typeof(BlogPost).Name} not found by expression");

            logger.LogDebug("finished successfuly");
            return entity;
        }
    }
}
