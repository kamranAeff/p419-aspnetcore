using Newtonsoft.Json;

namespace WebUI.Proxies
{
    public interface IBlogPostProxy
    {
        Task<ApiResponse<IEnumerable<BlogPost>>> GetAll();
    }

    public class BlogPostProxy : HttpClient, IBlogPostProxy
    {
        public BlogPostProxy(string url)
        {
            this.BaseAddress = new Uri(url);
        }

        public async Task<ApiResponse<IEnumerable<BlogPost>>> GetAll()
        {
            var response = await this.GetAsync("/api/blogposts");

            if (!response.IsSuccessStatusCode)
                throw new NotImplementedException();

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<BlogPost>>>(content);

            return data;
        }
    }







    public class BlogPost
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }

    public class ApiResponse<T>
        where T : class
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
    }
}
