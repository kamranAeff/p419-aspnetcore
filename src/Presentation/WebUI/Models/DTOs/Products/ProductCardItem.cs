namespace WebUI.Models.DTOs.Products
{
    public class ProductCardItem
    {
        public Guid? Id { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public bool IsDefault { get; set; }
    }
}
