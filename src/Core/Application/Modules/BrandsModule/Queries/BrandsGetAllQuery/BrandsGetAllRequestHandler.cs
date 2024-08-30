using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.BrandsModule.Queries.BrandsGetAllQuery
{
    class BrandsGetAllRequestHandler(IBrandRepository brandRepository) : IRequestHandler<BrandsGetAllRequest, IEnumerable<Brand>>
    {
        public async Task<IEnumerable<Brand>> Handle(BrandsGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = brandRepository.GetAll();

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
