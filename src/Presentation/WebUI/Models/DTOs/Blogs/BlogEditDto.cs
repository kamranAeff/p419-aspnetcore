namespace WebUI.Models.DTOs.Blogs
{
    public class BlogEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
