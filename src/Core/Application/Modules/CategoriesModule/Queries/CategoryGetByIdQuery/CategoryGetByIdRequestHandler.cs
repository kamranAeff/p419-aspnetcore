using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.CategoriesModule.Queries.CategoryGetByIdQuery
{
    class CategoryGetByIdRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetByIdRequest, Category>
    {
        public async Task<Category> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await categoryRepository.GetAsync(m=>m.Id == request.Id,cancellationToken);

            return entity;
        }
    }
}
