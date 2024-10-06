namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal
        {
            get => this.Price * this.Count;
        }
    }
}
