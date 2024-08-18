namespace Application.Modules.BlogPostsModule
{
    public class BlogPostResponse
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
