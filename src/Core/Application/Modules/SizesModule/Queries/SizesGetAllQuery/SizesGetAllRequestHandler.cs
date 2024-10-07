using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.SizesModule.Queries.SizesGetAllQuery
{
    class SizesGetAllRequestHandler(ISizeRepository sizeRepository) : IRequestHandler<SizesGetAllRequest, IEnumerable<Size>>
    {
        public async Task<IEnumerable<Size>> Handle(SizesGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = sizeRepository.GetAll();

            #region Filter
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(m => m.Name.StartsWith(request.Name));
            }
            if (request.Id > 0)
            {
                query = query.Where(m => m.Id == request.Id);
            }
            #endregion

            var response = await query.Sort(request)
                .ToListAsync(cancellationToken);
            return response;
        }
    }
}
