using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.SizesModule.Queries.SizesGetAllQuery
{
    public class SizesGetAllRequest : IRequest<IEnumerable<Size>>, ISortable
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
