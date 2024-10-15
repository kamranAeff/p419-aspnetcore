using WebUI.Models.Common;
using WebUI.Models.DTOs.Brands;
using WebUI.Services.Common;

namespace WebUI.Services.Brands
{
    class BrandService : ProxyService, IBrandService
    {
        public BrandService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<IEnumerable<Brand>>> GetAllAsync(CancellationToken cancellation = default) => base.GetAsync<ApiResponse<IEnumerable<Brand>>>("/api/brands", cancellation);

        public Task<ApiResponse<PagedResponse<Brand>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<PagedResponse<Brand>>>($"/api/brands/{page}/{size}", cancellation);

        public Task<ApiResponse<Brand>> GetByIdAsync(int id, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<Brand>>($"/api/brands/{id}", cancellation);

        public Task<ApiResponse> AddAsync(Brand request, CancellationToken cancellation = default) => base.PostAsync<Brand, ApiResponse>("/api/brands", request, cancellation);

        public Task<ApiResponse> EditAsync(Brand request, CancellationToken cancellation = default) => base.PutAsync<Brand, ApiResponse>($"/api/brands/{request.Id}", request, cancellation);

        public Task RemoveAsync(int id, CancellationToken cancellation = default) => base.DeleteAsync($"/api/brands/{id}", cancellation);
    }
}