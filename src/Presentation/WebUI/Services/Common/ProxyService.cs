using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using WebUI.Models.Common;

namespace WebUI.Services.Common
{
    public abstract class ProxyService
    {
        private readonly IHttpClientFactory httpClientFactory;
        protected HttpClient client;

        public ProxyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.client = httpClientFactory.CreateClient("httpClient");
            this.client.BaseAddress = new Uri(configuration["API_URL"]!);
        }


        public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellation = default)
            where T : ApiResponse
        {
            var response = await client.GetAsync(endpoint, cancellation);

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<T>(content)!;
            return apiResponse;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellation = default)
             where TResponse : ApiResponse
             where TRequest : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync(endpoint, content, cancellation);

            var contentJsonContent = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(contentJsonContent)!;

            return apiResponse;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellation = default)
            where TResponse : ApiResponse
            where TRequest : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PutAsync(endpoint, content, cancellation);

            var contentJsonContent = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<TResponse>(contentJsonContent)!;

            return apiResponse;
        }

        public async Task DeleteAsync(string endpoint, CancellationToken cancellation = default)
        {
            await client.DeleteAsync(endpoint, cancellation);
        }
    }
}
