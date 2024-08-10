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
            var response = await categoryRepository.GetAll().ToListAsync(cancellationToken);
            return response;
        }
    }
}
