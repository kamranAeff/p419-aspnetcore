using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
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

        public async Task<ApiResponse<IEnumerable<Brand>>> GetAllAsync(CancellationToken cancellation = default)
        {
            var response = await client.GetAsync("/api/brands", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Brand>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<PagedResponse<Brand>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/brands/{page}/{size}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PagedResponse<Brand>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<Brand>> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/brands/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Brand>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task AddAsync(Brand model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/api/brands", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task EditAsync(Brand model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PutAsync($"/api/brands/{model.Id}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.DeleteAsync($"/api/brands/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }
    }
}
