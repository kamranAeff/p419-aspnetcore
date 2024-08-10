using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.CategoriesModule.Commands.CategoryAddCommand
{
    class CategoryAddRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryAddRequest, Category>
    {
        public async Task<Category> Handle(CategoryAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Category
            {
                Name = request.Name
            };

            await categoryRepository.AddAsync(entity, cancellationToken);
            await categoryRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
