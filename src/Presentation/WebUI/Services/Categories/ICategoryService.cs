using WebUI.Models.Common;
using WebUI.Models.DTOs.Categories;

namespace WebUI.Services.Categories
{
    public interface ICategoryService
    {
        Task<ApiResponse<PagedResponse<Category>>> GetPagedAsync(int page, int size, CategoryGetPagedRequestDto request, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<Category>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task<ApiResponse> AddAsync(Category model, CancellationToken cancellation = default);
        Task<ApiResponse> EditAsync(Category model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
    }
}
