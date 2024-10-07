using Application.Common;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ColorsModule.Queries.ColorsPagedQuery
{
    class ColorsPagedRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorsPagedRequest, IPagedResponse<Color>>
    {
        public async Task<IPagedResponse<Color>> Handle(ColorsPagedRequest request, CancellationToken cancellationToken)
        {
            var query = colorRepository.GetAll();

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
