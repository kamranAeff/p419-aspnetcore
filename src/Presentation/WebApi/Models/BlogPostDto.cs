namespace WebApi.Models
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public required string Content { get; set; }
        public int CategoryId { get; set; }
        public string? PublishDate { get; set; }
    }
}
