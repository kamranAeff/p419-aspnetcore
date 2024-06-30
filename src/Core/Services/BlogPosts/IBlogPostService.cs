namespace Services.BlogPosts
{
    public interface IBlogPostService
    {
        Task<AddBlogPostResponseDto> AddAsync(AddBlogPostRequestDto request, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPostGetAllDto>> GetAll(CancellationToken cancellationToken = default);
        Task<BlogPostGetAllDto> GetById(int id, CancellationToken cancellationToken = default);
    }
}
