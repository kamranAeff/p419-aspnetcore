using WebUI.Models.Common;
using WebUI.Services.Common;
using bp = WebUI.Models.DTOs.Blogs;

namespace WebUI.Services.BlogPost
{
    class BlogPostService : ProxyService, IBlogPostService
    {
        public BlogPostService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<IEnumerable<bp.BlogPost>>> GetAllAsync(CancellationToken cancellation = default) => base.GetAsync<ApiResponse<IEnumerable<bp.BlogPost>>>("/api/blogposts", cancellation);

        public Task<ApiResponse<PagedResponse<bp.BlogPost>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<PagedResponse<bp.BlogPost>>>($"/api/blogposts/{page}/{size}", cancellation);

        public Task<ApiResponse<IEnumerable<bp.BlogPost>>> GetPopularsAsync(int recordCount, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<IEnumerable<bp.BlogPost>>>($"/api/blogposts/populars/{recordCount}", cancellation);

        public Task<ApiResponse<bp.BlogPost>> GetByIdAsync(int id, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<bp.BlogPost>>($"/api/blogposts/{id}", cancellation);

        public Task<ApiResponse<bp.BlogPost>> GetBySlugAsync(string slug, CancellationToken cancellation = default) => base.GetAsync<ApiResponse<bp.BlogPost>>($"/api/blogposts/{slug}", cancellation);

        public async Task AddAsync(bp.BlogAddDto model, CancellationToken cancellation = default)
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
        public async Task EditAsync(bp.BlogEditDto model, CancellationToken cancellation = default)
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

        public Task RemoveAsync(int id, CancellationToken cancellation = default) => base.DeleteAsync($"/api/blogposts/{id}", cancellation);
    }
}