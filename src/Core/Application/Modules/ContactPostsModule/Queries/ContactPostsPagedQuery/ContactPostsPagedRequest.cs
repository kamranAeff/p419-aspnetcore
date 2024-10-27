using Application.Common;
using Domain.Entities;
using Domain.StableModels;
using MediatR;

namespace Application.Modules.ContactPostsModule.Queries.ContactPostsPagedQuery
{
    public class ContactPostsPagedRequest : Pageable, IRequest<IPagedResponse<ContactPost>>, ISortable
    {
        public string Column => throw new NotImplementedException();

        public SortOrders Order => throw new NotImplementedException();
    }
}
