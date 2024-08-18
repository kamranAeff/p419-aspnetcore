using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Common;

namespace Application.Modules.ProductsModule.Commands.ProductEditCommand
{
    class ProductEditRequestHandler(IProductRepository productRepository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository,
        IFileService fileService)
        : IRequestHandler<ProductEditRequest, Product>
    {
        public async Task<Product> Handle(ProductEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await productRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            var brand = await brandRepository.GetAsync(m => m.Id == request.BrandId, cancellationToken);
            var category = await categoryRepository.GetAsync(m => m.Id == request.CategoryId, cancellationToken);

            entity.Title = request.Title;
            entity.BrandId = request.BrandId;
            entity.CategoryId = request.CategoryId;
            entity.Weight = request.Weight;
            entity.UnitOfWeight = request.UnitOfWeight;
            entity.Description = request.Description;
            entity.Information = request.Information;

            /*
                 3 sekil var bu mehsulda

                 I.   1-sekil silinir, 2 sekil elave olunur
                 II.  2 sekil elave olunur
                 III. 1-sekil silinir
                 IV.  sekillerde her hansi deyishiklik olmur
             
             */

            #region REMOVE from db
            var productImages = await productRepository.GetImages(m => m.ProductId == entity.Id)
                .ToListAsync(cancellationToken);

            var comingImageIds = request.Images.Where(m => m.Id is not null).Select(m => m.Id);

            foreach (var forRemove in productImages.Where(m => !comingImageIds.Contains(m.Id)))
            {
                productRepository.RemoveImage(forRemove);
                await fileService.ArchiveAsync($"{entity.Id}", forRemove.Path, cancellationToken);
            }
            #endregion

            #region ADD TO DB
            foreach (var forInsert in request.Images.Where(m => m.Id is null))
            {
                var image = new ProductImage
                {
                    IsMain = forInsert.IsMain,
                };

                image.Path = await fileService.UploadAsync(forInsert.Image);

                await productRepository.AddImageAsync(entity, image, cancellationToken);
            }
            #endregion

            #region for ISMAIN CHANGES 
            foreach (var forChange in productImages.Where(m => comingImageIds.Contains(m.Id)))
                forChange.IsMain = request.Images.FirstOrDefault(m => m.Id == forChange.Id).IsMain;
            #endregion

            await productRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
