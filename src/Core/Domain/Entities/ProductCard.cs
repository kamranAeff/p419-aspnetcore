namespace Domain.Entities
{
    public class ProductCard : AuditableEntity
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public bool IsDefault { get; set; }
    }
}
