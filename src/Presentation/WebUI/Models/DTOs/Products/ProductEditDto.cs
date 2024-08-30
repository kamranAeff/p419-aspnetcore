using WebUI.Models.Common;
using WebUI.Models.StableModels;

namespace WebUI.Models.DTOs.Products
{
    public class ProductEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal? Weight { get; set; }
        public Units UnitOfWeight { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public IEnumerable<ImageItem> Images { get; set; }
    }
}
