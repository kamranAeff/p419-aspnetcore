using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.BrandsModule.Queries.BrandsPagedQuery
{
    public class BrandsPagedRequest : Pageable, IRequest<IPagedResponse<Brand>>, ISortable
    {
        public IEnumerable<int> Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
