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



            //this.client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZXhwIjoxNzI5NTk5ODkzLCJpc3MiOiJhcGkub2dhbmkuYXoiLCJhdWQiOiJhcGkub2dhbmkuYXoifQ.UIVxtsABfjSzBp14UWjNCutxddUg_5u1nEe2e4QSktA");
        }
    }
}
