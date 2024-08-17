using Application.Common;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ProductsModule.Queries.ProductsGetAllQuery
{
    public class ProductsGetAllRequest : IRequest<IEnumerable<ProductsResponse>>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
