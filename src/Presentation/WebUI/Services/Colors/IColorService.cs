using WebUI.Models.Common;
using WebUI.Models.DTOs.Colors;

namespace WebUI.Services.Colors
{
    public interface IColorService
    {
        Task<ApiResponse<PagedResponse<Color>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Color>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<Color>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task<ApiResponse> AddAsync(Color model, CancellationToken cancellation = default);
        Task<ApiResponse> EditAsync(Color model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
