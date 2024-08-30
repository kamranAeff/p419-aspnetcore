using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
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

        public async Task<ApiResponse<PagedResponse<Category>>> GetPagedAsync(int page, int size, CategoryGetPagedRequestDto request, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync($"/api/categories/{page}/{size}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var contentjsonContent = await response.Content.ReadAsStringAsync(cancellation);
            Console.WriteLine(contentjsonContent);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PagedResponse<Category>>>(contentjsonContent)!;
            Console.WriteLine(contentjsonContent);
            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<IEnumerable<Category>>> GetAllAsync(CancellationToken cancellation = default)
        {
            var response = await client.GetAsync("/api/categories", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Category>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<Category>> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/categories/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Category>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }


        public async Task AddAsync(Category model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/api/categories", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task EditAsync(Category model, CancellationToken cancellation = default)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PutAsync($"/api/categories/{model.Id}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.DeleteAsync($"/api/categories/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }
    }
}
