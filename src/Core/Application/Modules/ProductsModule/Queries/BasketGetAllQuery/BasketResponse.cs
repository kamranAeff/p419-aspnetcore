namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    public class BasketResponse
    {
        public IEnumerable<BasketItem> List { get; set; }
        public decimal Total { get; set; }
        public string Coupon { get; set; }
        public decimal DiscountedTotal { get; set; }
    }
}
