using Application.Extensions;
using Application.Services;
using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ProductsModule.Commands.ProductAddCommand
{
    class ProductAddRequestHandler(IProductRepository productRepository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository,
        IFileService fileService)
        : IRequestHandler<ProductAddRequest, Product>
    {
        public async Task<Product> Handle(ProductAddRequest request, CancellationToken cancellationToken)
        {
            var brand = await brandRepository.GetAsync(m => m.Id == request.BrandId, cancellationToken);
            var category = await categoryRepository.GetAsync(m => m.Id == request.CategoryId, cancellationToken);

            var entity = new Product
            {
                Title = request.Title,
                Slug = request.Title.ToSlug(),
                BrandId = brand.Id,
                CategoryId = category.Id,
                Rate = 5,
                Weight = request.Weight,
                UnitOfWeight = request.UnitOfWeight,
                Description = request.Description,
                Information = request.Information,
            };

            await productRepository.AddAsync(entity, cancellationToken);
            await productRepository.SaveAsync(cancellationToken);

            if (request.Images is not null)
            {
                foreach (var item in request.Images)
                {
                    var image = new ProductImage
                    {
                        IsMain = item.IsMain
                    };

                    image.Path = await fileService.UploadAsync(item.File);
                    await productRepository.AddImageAsync(entity, image, cancellationToken);
                }

                await productRepository.SaveAsync(cancellationToken);
            }

            return entity;
        }
    }
}
