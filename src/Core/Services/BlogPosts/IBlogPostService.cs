using Domain.Entities;

namespace Services.BlogPosts
{
    public interface IBlogPostService
    {
        Task<AddBlogPostResponseDto> AddAsync(AddBlogPostRequestDto request, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
