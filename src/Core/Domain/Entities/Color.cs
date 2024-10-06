namespace Domain.Entities
{
    public class Color : AuditableEntity
    {
        public int Id { get; set; }
        public required string HexCode { get; set; }
        public required string Name { get; set; }
    }
}
