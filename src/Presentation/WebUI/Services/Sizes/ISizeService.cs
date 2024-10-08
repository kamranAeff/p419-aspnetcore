using WebUI.Models.Common;
using WebUI.Models.DTOs.Sizes;

namespace WebUI.Services.Sizes
{
    public interface ISizeService
    {
        Task<ApiResponse<PagedResponse<Size>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Size>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<Size>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task<ApiResponse> AddAsync(Size model, CancellationToken cancellation = default);
        Task<ApiResponse> EditAsync(Size model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
