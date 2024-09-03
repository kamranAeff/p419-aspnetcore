using WebUI.Models.Common;
using WebUI.Models.DTOs.Blogs;

namespace WebUI.Services.BlogPost
{
    public interface IBlogPostService
    {
        Task<ApiResponse<PagedResponse<Models.DTOs.Blogs.BlogPost>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Models.DTOs.Blogs.BlogPost>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Models.DTOs.Blogs.BlogPost>>> GetPopularsAsync(int recordCount,CancellationToken cancellation = default);
        Task<ApiResponse<Models.DTOs.Blogs.BlogPost>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task<ApiResponse<Models.DTOs.Blogs.BlogPost>> GetBySlugAsync(string slug, CancellationToken cancellation = default);
        Task AddAsync(BlogAddDto model, CancellationToken cancellation = default);
        Task EditAsync(BlogEditDto model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
