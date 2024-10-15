using WebUI.Models.Common;
using WebUI.Models.DTOs.Categories;
using WebUI.Services.Common;

namespace WebUI.Services.Categories
{
    class CategoryService : ProxyService, ICategoryService
    {
        public CategoryService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<PagedResponse<Category>>> GetPagedAsync(int page, int size, CategoryGetPagedRequestDto request, CancellationToken cancellation = default)
            => base.PostAsync<CategoryGetPagedRequestDto, ApiResponse<PagedResponse<Category>>>($"/api/categories/{page}/{size}", request, cancellation);

        public Task<ApiResponse<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<IEnumerable<Category>>>("/api/categories", cancellation);

        public Task<ApiResponse<Category>> GetByIdAsync(int id, CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<Category>>($"/api/categories/{id}", cancellation);

        public Task<ApiResponse> AddAsync(Category request, CancellationToken cancellation = default)
            => base.PostAsync<Category, ApiResponse>("/api/categories", request, cancellation);

        public Task<ApiResponse> EditAsync(Category request, CancellationToken cancellation = default)
            => base.PutAsync<Category, ApiResponse>($"/api/categories/{request.Id}", request, cancellation);

        public Task RemoveAsync(int id, CancellationToken cancellation = default)
            => base.DeleteAsync($"/api/categories/{id}", cancellation);
    }
}