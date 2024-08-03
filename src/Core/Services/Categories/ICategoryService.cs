﻿namespace Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryDto> GetByIdAsync(int id,CancellationToken cancellationToken = default);
        Task<AddCategoryResponseDto> AddAsync(AddCategoryRequestDto model, CancellationToken cancellationToken = default);
        Task<EditCategoryDto> EditAsync(EditCategoryDto model, CancellationToken cancellationToken = default);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
