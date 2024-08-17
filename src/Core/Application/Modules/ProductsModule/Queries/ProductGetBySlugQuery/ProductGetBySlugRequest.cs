using MediatR;

namespace Application.Modules.ProductsModule.Queries.ProductGetBySlugQuery
{
    public class ProductGetBySlugRequest : IRequest<ProductsResponse>
    {
        public string Slug { get; set; }
    }
}
