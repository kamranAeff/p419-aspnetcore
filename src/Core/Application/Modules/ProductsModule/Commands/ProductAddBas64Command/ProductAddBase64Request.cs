using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ProductsModule.Commands.ProductAddBase64Command
{
    public class ProductAddBase64Request : IRequest<Product>
    {
        public string Title { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal? Weight { get; set; }
        public Units UnitOfWeight { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public IEnumerable<ImageItemBase64> Files { get; set; }
        public IEnumerable<ProductCardItem> Cards { get; set; }
    }
}
