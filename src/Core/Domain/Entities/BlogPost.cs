namespace Domain.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public required string Body { get; set; }
        public int CategoryId { get; set; }
        public int? PublisherId { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
