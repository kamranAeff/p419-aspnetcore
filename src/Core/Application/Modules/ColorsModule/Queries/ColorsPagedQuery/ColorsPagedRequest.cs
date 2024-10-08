using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ColorsModule.Queries.ColorsPagedQuery
{
    public class ColorsPagedRequest : Pageable, IRequest<IPagedResponse<Color>>, ISortable
    {
        public IEnumerable<int> Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
