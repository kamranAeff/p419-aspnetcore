using WebUI.Models.Common;
using WebUI.Models.DTOs.Products;

namespace WebUI.Services.Products
{
    public interface IProductService
    {
        Task<ApiResponse<PagedResponse<Product>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default);
        Task<ApiResponse<IEnumerable<Product>>> GetAllAsync(CancellationToken cancellation = default);
        Task<ApiResponse<ProductDetail>> GetByIdAsync(int id, CancellationToken cancellation = default);
        Task AddAsync(ProductAddDto model, CancellationToken cancellation = default);
        Task EditAsync(ProductEditDto model, CancellationToken cancellation = default);
        Task RemoveAsync(int id, CancellationToken cancellation = default);
        Task<ApiResponse<BasketResponse>> BasketInteractAsync(BasketInteractDto model, CancellationToken cancellation = default);
        Task<ApiResponse<BasketResponse>> BasketGetAllAsync(CancellationToken cancellation = default);
    }
}