using Application.Common;
using Application.Modules.ProductsModule.Commands.ProductAddCommand;
using Domain.StableModels;

namespace Application.Modules.ProductsModule.Queries
{
    public class ProductsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Rate { get; set; }
        public decimal? Weight { get; set; }
        public Units UnitOfWeight { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public IEnumerable<ImageItem> Images { get; set; }
    }
}
