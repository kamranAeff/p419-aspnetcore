using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.CategoriesModule.Commands.CategoryEditCommand
{
    class CategoryEditRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryEditRequest, Category>
    {
        public async Task<Category> Handle(CategoryEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await categoryRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
           
            entity.Name = request.Name;

            await categoryRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
