namespace WebUI.Models.DTOs.Products
{
    public class BasketResponse
    {
        public List<BasketItem> Items { get; set; }

        public double Total { get; set; }

        public string Coupon { get; set; }

        public double DiscountedTotal { get; set; }
    }

    public class BasketItem
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double Subtotal { get; set; }
    }
}
