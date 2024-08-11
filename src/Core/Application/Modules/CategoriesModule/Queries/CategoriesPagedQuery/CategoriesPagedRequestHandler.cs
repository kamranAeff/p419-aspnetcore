using Application.Common;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.CategoriesModule.Queries.CategoriesPagedQuery
{
    class CategoriesPagedRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoriesPagedRequest, IPagedResponse<Category>>
    {
        public async Task<IPagedResponse<Category>> Handle(CategoriesPagedRequest request, CancellationToken cancellationToken)
        {
            var query = categoryRepository.GetAll();

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
