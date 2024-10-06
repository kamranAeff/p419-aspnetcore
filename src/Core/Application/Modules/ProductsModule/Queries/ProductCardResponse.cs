namespace Application.Modules.ProductsModule.Queries
{
    public class ProductCardResponse
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public int ColorId { get; set; }
        public string Color { get; set; }
        public string ColorHex { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public bool IsDefault { get; set; }
    }
}
