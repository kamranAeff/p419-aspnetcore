namespace Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
