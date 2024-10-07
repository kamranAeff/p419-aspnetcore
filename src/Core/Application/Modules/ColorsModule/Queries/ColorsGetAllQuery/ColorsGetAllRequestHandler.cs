using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ColorsModule.Queries.ColorsGetAllQuery
{
    class ColorsGetAllRequestHandler(IColorRepository colorRepository) : IRequestHandler<ColorsGetAllRequest, IEnumerable<Color>>
    {
        public async Task<IEnumerable<Color>> Handle(ColorsGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = colorRepository.GetAll();

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
