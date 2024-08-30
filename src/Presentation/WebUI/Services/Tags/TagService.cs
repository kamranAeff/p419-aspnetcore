using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
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

        public async Task<ApiResponse<IEnumerable<Tag>>> GetAllAsync(CancellationToken cancellation = default)
        {
            var response = await client.GetAsync("/api/tags", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Tag>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<PagedResponse<Tag>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/tags/{page}/{size}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PagedResponse<Tag>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<Tag>> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/tags/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Tag>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task AddAsync(Tag model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/api/tags", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task EditAsync(Tag model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PutAsync($"/api/tags/{model.Id}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.DeleteAsync($"/api/tags/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }
    }
}
