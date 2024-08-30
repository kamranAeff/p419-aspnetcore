using Azure;
using Newtonsoft.Json;
using WebUI.Models.Common;
using WebUI.Models.DTOs.Blogs;
using WebUI.Services.Common;

namespace WebUI.Services.BlogPost
{
    class BlogPostService : ProxyService, IBlogPostService
    {
        public BlogPostService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public async Task<ApiResponse<IEnumerable<Models.DTOs.Blogs.BlogPost>>> GetAllAsync(CancellationToken cancellation = default)
        {
            var response = await client.GetAsync("/api/blogposts", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Models.DTOs.Blogs.BlogPost>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<PagedResponse<Models.DTOs.Blogs.BlogPost>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/blogposts/{page}/{size}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PagedResponse<Models.DTOs.Blogs.BlogPost>>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<Models.DTOs.Blogs.BlogPost>> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/blogposts/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Models.DTOs.Blogs.BlogPost>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task<ApiResponse<Models.DTOs.Blogs.BlogPost>> GetBySlugAsync(string slug, CancellationToken cancellation = default)
        {
            var response = await client.GetAsync($"/api/blogposts/{slug}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Models.DTOs.Blogs.BlogPost>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }

        public async Task AddAsync(BlogAddDto model, CancellationToken cancellation = default)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Title), nameof(model.Title));
            content.Add(new StringContent(model.Body), nameof(model.Body));
            content.Add(new StringContent(model.CategoryId.ToString()), nameof(model.CategoryId));

            if (model.Image is not null) 
                content.Add(new StreamContent(model.Image.OpenReadStream()), nameof(model.Image), model.Image.FileName);

            var response = await client.PostAsync("/api/blogposts", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }
        public async Task EditAsync(BlogEditDto model, CancellationToken cancellation = default)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Title), nameof(model.Title));
            content.Add(new StringContent(model.Body), nameof(model.Body));
            content.Add(new StringContent(model.ImagePath), nameof(model.ImagePath));
            content.Add(new StringContent(model.CategoryId.ToString()), nameof(model.CategoryId));

            if (model.Image is not null)
                content.Add(new StreamContent(model.Image.OpenReadStream()), nameof(model.Image), model.Image.FileName);

            var response = await client.PutAsync($"/api/blogposts/{model.Id}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            var response = await client.DeleteAsync($"/api/blogposts/{id}", cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }
    }
}
