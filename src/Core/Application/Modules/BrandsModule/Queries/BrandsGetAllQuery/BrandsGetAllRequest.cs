using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.BrandsModule.Queries.BrandsGetAllQuery
{
    public class BrandsGetAllRequest : IRequest<IEnumerable<Brand>>, ISortable
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
