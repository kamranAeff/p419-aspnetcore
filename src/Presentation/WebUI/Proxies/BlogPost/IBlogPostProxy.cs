namespace WebUI.Proxies.BlogPostProxy
{
    public interface IBlogPostProxy
    {
        Task<ApiResponse<IEnumerable<BlogPost>>> GetAll();
    }
}
