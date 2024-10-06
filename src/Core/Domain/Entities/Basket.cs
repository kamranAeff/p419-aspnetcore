namespace Domain.Entities
{
    public class Basket
    {
        public int UserId { get; set; }
        public Guid ProductCardId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
