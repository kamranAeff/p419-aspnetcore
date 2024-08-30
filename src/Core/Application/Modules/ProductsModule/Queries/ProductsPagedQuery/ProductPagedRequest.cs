using Application.Common;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ProductsModule.Queries.ProductPagesQuery
{
    public class ProductPagedRequest : Pageable,IRequest<IPagedResponse<ProductsResponse>>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
