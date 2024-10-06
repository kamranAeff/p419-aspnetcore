namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSlug { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal
        {
            get => this.Price * this.Count;
        }
    }
}
