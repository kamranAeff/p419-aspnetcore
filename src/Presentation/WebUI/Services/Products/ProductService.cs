using WebUI.Extensions;
using WebUI.Models.Common;
using WebUI.Models.DTOs.Products;
using WebUI.Services.Common;

namespace WebUI.Services.Products
{
    class ProductService : ProxyService, IProductService
    {
        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<IEnumerable<Product>>> GetAllAsync(CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<IEnumerable<Product>>>("/api/products", cancellation);

        public Task<ApiResponse<PagedResponse<Product>>> GetPagedAsync(int page, int size, CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<PagedResponse<Product>>>($"/api/products/{page}/{size}", cancellation);

        public Task<ApiResponse<ProductDetail>> GetByIdAsync(int id, CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<ProductDetail>>($"/api/products/{id}", cancellation);

        public async Task AddAsync(ProductAddDto model, CancellationToken cancellation = default)
        {
            var content = new MultipartFormDataContent()
                .AddString(model.Title, nameof(model.Title))
                .AddInt(model.BrandId, nameof(model.BrandId))
                .AddInt(model.CategoryId, nameof(model.CategoryId))
                .AddByte((byte)model.UnitOfWeight, nameof(model.UnitOfWeight))
                .AddDecimal(model.Weight, nameof(model.Weight))
                .AddString(model.Description, nameof(model.Description))
                .AddString(model.Information, nameof(model.Information));

            if (model.Images is not null)
                for (int i = 0; i < model.Images.Length; i++)
                {
                    content.AddInt(model.Images[i].Id, $"Images[{i}].Id")
                            .AddFile(model.Images[i].File, $"Images[{i}].File")
                            .AddBoolean(model.Images[i].IsMain, $"Images[{i}].IsMain")
                            .AddString(model.Images[i].TempPath, $"Images[{i}].TempPath");
                }

            if (model.Cards is not null)
                for (int i = 0; i < model.Cards.Length; i++)
                {
                    content.AddInt(model.Cards[i].SizeId, $"cards[{i}].SizeId")
                        .AddInt(model.Cards[i].ColorId, $"cards[{i}].ColorId")
                        .AddDecimal(model.Cards[i].Price, $"cards[{i}].Price")
                        .AddBoolean(model.Cards[i].IsDefault, $"cards[{i}].IsDefault");

                    if (model.Cards[i].Id is not null)
                        content.AddString(model.Cards[i].Id.ToString()!, $"Cards[{i}].Id");
                }

            var response = await client.PostAsync("/api/products", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public async Task EditAsync(ProductEditDto model, CancellationToken cancellation = default)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent($"{model.Id}"), nameof(model.Id));
            content.Add(new StringContent(model.Title), nameof(model.Title));
            content.Add(new StringContent($"{model.BrandId}"), nameof(model.BrandId));
            content.Add(new StringContent($"{model.CategoryId}"), nameof(model.CategoryId));
            content.Add(new StringContent($"{(byte)model.UnitOfWeight}"), nameof(model.UnitOfWeight));

            if (model.Weight is not null)
                content.Add(new StringContent($"{model.Weight}"), nameof(model.Weight));

            if (!string.IsNullOrWhiteSpace(model.Description))
                content.Add(new StringContent($"{model.Description}"), nameof(model.Description));

            if (!string.IsNullOrWhiteSpace(model.Information))
                content.Add(new StringContent($"{model.Information}"), nameof(model.Information));

            if (model.Images is not null)
            {
                int index = 0;
                bool approved = false;

                foreach (var item in model.Images)
                {
                    approved = false;

                    if (item.Id is not null)
                    {
                        content.Add(new StringContent($"{item.Id}"), $"Images[{index}].Id");
                        approved = true;
                    }

                    if (item.File is not null)
                    {
                        content.Add(new StreamContent(item.File.OpenReadStream()), $"Images[{index}].File", item.File.FileName);
                        approved = true;
                    }

                    if (!string.IsNullOrWhiteSpace(item.TempPath))
                    {
                        content.Add(new StringContent(item.TempPath), $"Images[{index}].TempPath");
                        approved = true;
                    }

                    if (item.IsMain)
                    {
                        content.Add(new StringContent($"{item.IsMain}"), $"Images[{index}].IsMain");
                        approved = true;
                    }

                    if (approved)
                        index++;
                }
            }

            var response = await client.PutAsync($"/api/products/{model.Id}", content, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");
        }

        public Task RemoveAsync(int id, CancellationToken cancellation = default) => base.DeleteAsync($"/api/products/{id}", cancellation);

        public Task<ApiResponse<BasketResponse>> BasketInteractAsync(BasketInteractDto model, CancellationToken cancellation = default)
            => base.PostAsync<BasketInteractDto, ApiResponse<BasketResponse>>("/api/products/basket-interact", model, cancellation);

        public Task<ApiResponse<BasketResponse>> BasketGetAllAsync(CancellationToken cancellation = default)
            => base.GetAsync<ApiResponse<BasketResponse>>("/api/products/basket", cancellation);
    }
}