using Application.Common;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.SizesModule.Queries.SizesPagedQuery
{
    class SizesPagedRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizesPagedRequest, IPagedResponse<Size>>
    {
        public async Task<IPagedResponse<Size>> Handle(SizesPagedRequest request, CancellationToken cancellationToken)
        {
            var query = sizeRepository.GetAll();

            #region Filter
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(m => m.Name.StartsWith(request.Name));
            }
            if (request.Id?.Count()>0)
            {
                query = query.Where(m => request.Id.Contains(m.Id));
            }
            #endregion

            var response = await query.Sort(request)
                .ToPaginateAsync(request, cancellationToken);

            return response;
        }
    }
}
