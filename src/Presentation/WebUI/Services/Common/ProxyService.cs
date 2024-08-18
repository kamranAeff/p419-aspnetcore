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
    }
}
