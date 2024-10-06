namespace Domain.Entities
{
    public class Size : AuditableEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SmallName { get; set; }
    }
}
