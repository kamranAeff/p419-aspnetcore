using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.CategoriesModule.Queries.CategoriesPagedQuery
{
    public class CategoriesPagedRequest : Pageable, IRequest<IPagedResponse<Category>>, ISortable
    {
        public IEnumerable<int> Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
