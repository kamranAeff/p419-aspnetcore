using Application.Common;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.BrandsModule.Queries.BrandsPagedQuery
{
    class BrandsPagedRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandsPagedRequest, IPagedResponse<Brand>>
    {
        public async Task<IPagedResponse<Brand>> Handle(BrandsPagedRequest request, CancellationToken cancellationToken)
        {
            var query = brandRepository.GetAll();

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
