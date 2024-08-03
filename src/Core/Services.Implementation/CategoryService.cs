using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Categories;

namespace Services.Implementation
{
    class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<AddCategoryResponseDto> AddAsync(AddCategoryRequestDto model, CancellationToken cancellationToken = default)
        {
            var entity = new Category { Name = model.Name };

            await categoryRepository.AddAsync(entity, cancellationToken);
            await categoryRepository.SaveAsync(cancellationToken);

            return new AddCategoryResponseDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<EditCategoryDto> EditAsync(EditCategoryDto model, CancellationToken cancellationToken = default)
        {
            var entity = await categoryRepository.GetAsync(m => m.Id == model.Id, cancellationToken);

            entity.Name = model.Name;

            categoryRepository.Edit(entity);
            await categoryRepository.SaveAsync(cancellationToken);

            return model;
        }

        public async Task<IEnumerable<CategoryGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await categoryRepository.GetAll()
                .Select(m => new CategoryGetAllDto
                {
                    Id = m.Id,
                    Name = m.Name,
                })
                .ToListAsync(cancellationToken);

            return data;
        }

        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await categoryRepository.GetAsync(m => m.Id == id, cancellationToken);

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await categoryRepository.GetAsync(m => m.Id == id, cancellationToken);
            categoryRepository.Remove(entity);
            await categoryRepository.SaveAsync(cancellationToken);
        }
    }
}
