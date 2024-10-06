namespace Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public int Id { get; set; }
        public required string Text { get; set; }
    }
}
