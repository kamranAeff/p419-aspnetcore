using MediatR;

namespace Application.Modules.ProductsModule.Queries.ProductGetByIdQuery
{
    public class ProductGetByIdRequest : IRequest<ProductsResponse>
    {
        public int Id { get; set; }
    }
}
