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
            var response = await categoryRepository.GetAll()
                .Sort(request)
                .ToPaginateAsync(request, cancellationToken);

            return response;
        }
    }
}
