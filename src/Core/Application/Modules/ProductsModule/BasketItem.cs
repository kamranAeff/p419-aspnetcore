namespace Application.Modules.ProductsModule
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal
        {
            get => Price * Count;
        }
    }
}
