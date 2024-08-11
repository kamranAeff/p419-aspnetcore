using Newtonsoft.Json;

namespace WebUI.Proxies.BlogPostProxy
{
    public class BlogPostProxy : HttpClient, IBlogPostProxy
    {
        public BlogPostProxy(string url)
        {
            BaseAddress = new Uri(url);
        }

        public async Task<ApiResponse<IEnumerable<BlogPost>>> GetAll()
        {
            var response = await GetAsync("/api/blogposts");

            if (!response.IsSuccessStatusCode)
                throw new NotImplementedException();

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<BlogPost>>>(content);

            return data;
        }
    }
}
