using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.CategoriesModule.Queries.CategoriesGetAllQuery
{
    class CategoriesGetAllRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoriesGetAllRequest, IEnumerable<Category>>
    {
        public async Task<IEnumerable<Category>> Handle(CategoriesGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = categoryRepository.GetAll();

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
