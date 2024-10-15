using WebUI.Models.Common;
using WebUI.Models.DTOs.Colors;
using WebUI.Services.Common;

namespace WebUI.Services.Colors
{
    class ColorService : ProxyService, IColorService
    {
        public ColorService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration) { }

        public Task<ApiResponse<IEnumerable<Color>>> GetAllAsync(CancellationToken cancellation = default) => base.GetAsync<ApiResponse<IEnumerable<Color>>>("/api/colors", cancellation);

        public Task<ApiResponse<PagedResponse<Color>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<PagedResponse<Color>>>($"/api/colors/{page}/{size}", cancellation);

        public Task<ApiResponse<Color>> GetByIdAsync(int id, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<Color>>($"/api/colors/{id}", cancellation);

        public Task<ApiResponse> AddAsync(Color request, CancellationToken cancellation = default) => base.PostAsync<Color, ApiResponse>("/api/colors", request, cancellation);

        public Task<ApiResponse> EditAsync(Color request, CancellationToken cancellation = default) => base.PutAsync<Color, ApiResponse>($"/api/colors/{request.Id}", request, cancellation);

        public Task RemoveAsync(int id, CancellationToken cancellation = default) => base.DeleteAsync($"/api/colors/{id}", cancellation);
    }
}