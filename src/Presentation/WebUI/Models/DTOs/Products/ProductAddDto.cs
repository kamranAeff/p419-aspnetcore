using WebUI.Models.Common;
using WebUI.Models.StableModels;

namespace WebUI.Models.DTOs.Products
{
    public class ProductAddDto
    {
        public string Title { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal? Weight { get; set; }
        public Units UnitOfWeight { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public ImageItem[] Images { get; set; }
        public ProductCardItem[] Cards { get; set; }
    }
}
