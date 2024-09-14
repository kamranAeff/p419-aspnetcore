using WebUI.Models.Common;
using WebUI.Models.DTOs.Tags;
using WebUI.Services.Common;

namespace WebUI.Services.Tags
{
    class TagService : ProxyService, ITagService
    {
        public TagService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<IEnumerable<Tag>>> GetAllAsync(CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<IEnumerable<Tag>>>("/api/tags", cancellation);

        public Task<ApiResponse<PagedResponse<Tag>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<PagedResponse<Tag>>>($"/api/tags/{page}/{size}", cancellation);

        public Task<ApiResponse<Tag>> GetByIdAsync(int id, CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<Tag>>($"/api/tags/{id}", cancellation);

        public Task<ApiResponse> AddAsync(Tag request, CancellationToken cancellation = default)
            => base.PostAsync<Tag, ApiResponse>("/api/tags", request, cancellation);

        public Task<ApiResponse> EditAsync(Tag request, CancellationToken cancellation = default)
            => base.PutAsync<Tag, ApiResponse>($"/api/tags/{request.Id}", request, cancellation);

        public Task RemoveAsync(int id, CancellationToken cancellation = default)
            => base.DeleteAsync($"/api/tags/{id}", cancellation);
    }
}
