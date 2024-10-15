using WebUI.Models.Common;
using WebUI.Models.DTOs.Sizes;
using WebUI.Services.Common;

namespace WebUI.Services.Sizes
{
    class SizeService : ProxyService, ISizeService
    {
        public SizeService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration) { }

        public Task<ApiResponse<IEnumerable<Size>>> GetAllAsync(CancellationToken cancellation = default) => base.GetAsync<ApiResponse<IEnumerable<Size>>>("/api/sizes", cancellation);

        public Task<ApiResponse<PagedResponse<Size>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<PagedResponse<Size>>>($"/api/sizes/{page}/{size}", cancellation);

        public Task<ApiResponse<Size>> GetByIdAsync(int id, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<Size>>($"/api/sizes/{id}", cancellation);

        public Task<ApiResponse> AddAsync(Size request, CancellationToken cancellation = default) => base.PostAsync<Size, ApiResponse>("/api/sizes", request, cancellation);

        public Task<ApiResponse> EditAsync(Size request, CancellationToken cancellation = default) => base.PutAsync<Size, ApiResponse>($"/api/sizes/{request.Id}", request, cancellation);

        public Task RemoveAsync(int id, CancellationToken cancellation = default) => base.DeleteAsync($"/api/sizes/{id}", cancellation);
    }
}