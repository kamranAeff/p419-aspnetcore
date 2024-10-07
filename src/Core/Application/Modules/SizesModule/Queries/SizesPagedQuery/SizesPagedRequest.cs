using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.SizesModule.Queries.SizesPagedQuery
{
    public class SizesPagedRequest : Pageable, IRequest<IPagedResponse<Size>>, ISortable
    {
        public IEnumerable<int> Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
