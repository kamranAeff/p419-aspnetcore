namespace Domain.Entities
{
    public class ContactPost : ICreateEntity
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? AnsweredBy { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public string? AnsweredMessage { get; set; }
    }
}
