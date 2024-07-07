using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.BlogPosts
{
    public class AddBlogPostRequestDto
    {
        public required IFormFile Image { get; set; }
        public required string Body { get; set; }
        public int? CategoryId { get; set; }
    }

    public class AddBlogPostResponseDto
    {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Body { get; set; }
        public int CategoryId { get; set; }

        public static AddBlogPostResponseDto Create(BlogPost entity)
        {
            return new AddBlogPostResponseDto
            {
                Id = entity.Id,
                ImagePath = entity.ImagePath,
                Body = entity.Body,
                CategoryId = entity.CategoryId,
            };
        }
    }

    public class BlogPostGetAllDto
    {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Body { get; set; }
        public int CategoryId { get; set; }

        public static BlogPostGetAllDto Create(BlogPost entity)
        {
            return new BlogPostGetAllDto
            {
                Id = entity.Id,
                ImagePath = entity.ImagePath,
                Body = entity.Body,
                CategoryId = entity.CategoryId,
            };
        }
    }
}
