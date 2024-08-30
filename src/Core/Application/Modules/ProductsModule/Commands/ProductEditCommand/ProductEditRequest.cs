using Application.Common;
using Application.Modules.ProductsModule.Commands.ProductAddCommand;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ProductsModule.Commands.ProductEditCommand
{
    public class ProductEditRequest : IRequest<Product>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal? Weight { get; set; }
        public Units UnitOfWeight { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public List<ImageItem> Images { get; set; }
    }
}
