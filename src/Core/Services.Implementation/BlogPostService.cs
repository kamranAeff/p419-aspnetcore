using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repositories;
using Services.BlogPosts;

namespace Services.Implementation
{
    class BlogPostService(IBlogPostRepository blogPostRepository, IHostEnvironment env) : IBlogPostService
    {
        public async Task<AddBlogPostResponseDto> AddAsync(AddBlogPostRequestDto request, CancellationToken cancellationToken = default)
        {
            var entity = new BlogPost
            {
                Body = request.Body,
                CategoryId = request.CategoryId ?? 1
            };

            var extension = Path.GetExtension(request.Image.FileName); // .jpg,  .jpeg
            entity.ImagePath = $"{Guid.NewGuid()}{extension}"; //B1EA73E3-E115-4689-A77B-D74A35F0DD55.jpg
            string fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", entity.ImagePath);

            Console.WriteLine(fullPath);

            using (var fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write))
            {
                await request.Image.CopyToAsync(fs);
            }

            await blogPostRepository.AddAsync(entity, cancellationToken);
            await blogPostRepository.SaveAsync(cancellationToken);

            return AddBlogPostResponseDto.Create(entity);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await blogPostRepository.GetAll()
                .ToListAsync(cancellationToken);
        }

        public async Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await blogPostRepository.GetAsync(m => m.Id == id, cancellationToken);

            return entity;
        }
    }
}
