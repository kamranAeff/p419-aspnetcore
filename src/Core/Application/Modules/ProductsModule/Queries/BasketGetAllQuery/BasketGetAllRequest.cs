using Application.Common;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    public class BasketGetAllRequest : IRequest<BasketResponse>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
