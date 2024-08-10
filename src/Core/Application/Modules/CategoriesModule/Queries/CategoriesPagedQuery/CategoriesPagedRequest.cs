using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.CategoriesModule.Queries.CategoriesPagedQuery
{
    public class CategoriesPagedRequest : Pageable, IRequest<IPagedResponse<Category>>, ISortable
    {
        public string Column { get; set; }

        public SortOrders Order { get; set; }
    }
}
