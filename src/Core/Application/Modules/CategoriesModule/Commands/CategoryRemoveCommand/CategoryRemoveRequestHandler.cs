using MediatR;
using Repositories;

namespace Application.Modules.CategoriesModule.Commands.CategoryRemoveCommand
{
    class CategoryRemoveRequestHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryRemoveRequest>
    {
        public async Task Handle(CategoryRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await categoryRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            categoryRepository.Remove(entity);
            await categoryRepository.SaveAsync(cancellationToken);
        }
    }
}
