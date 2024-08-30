using WebUI.Models.Common;
using WebUI.Models.DTOs.Tags;

namespace WebUI.Services.Tags
{
    public interface ITagService
    {
        Task<ApiResponse<PagedResponse<Tag>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Tag>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<Tag>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(Tag model, CancellationToken cancellation = default);
        Task EditAsync(Tag model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
