using Domain.StableModels;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int BrandId { get; set; }
        public required int CategoryId { get; set; }
        public decimal Rate { get; set; }
        public decimal? Weight { get; set; }
        public required Units UnitOfWeight { get; set; }
        public required string Description { get; set; }
        public required string Information { get; set; }
    }
}
