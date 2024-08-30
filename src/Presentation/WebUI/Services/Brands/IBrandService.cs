using WebUI.Models.Common;
using WebUI.Models.DTOs.Brands;

namespace WebUI.Services.Brands
{
    public interface IBrandService
    {
        Task<ApiResponse<PagedResponse<Brand>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Brand>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<Brand>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(Brand model, CancellationToken cancellation = default);
        Task EditAsync(Brand model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
