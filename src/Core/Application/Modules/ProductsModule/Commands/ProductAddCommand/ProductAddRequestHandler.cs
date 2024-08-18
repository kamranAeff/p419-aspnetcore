using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Common;

namespace Application.Modules.ProductsModule.Commands.ProductAddCommand
{
    class ProductAddRequestHandler(IProductRepository productRepository,
        IBrandRepository brandRepository,
        IFileService fileService)
        : IRequestHandler<ProductAddRequest, Product>
    {
        public async Task<Product> Handle(ProductAddRequest request, CancellationToken cancellationToken)
        {
            var brand = await brandRepository.GetAsync(m => m.Id == request.BrandId, cancellationToken);

            var entity = new Product
            {
                Title = request.Title,
                Slug = request.Title.ToSlug(),
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
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
                        IsMain = item.IsMain,
                    };

                    image.Path = await fileService.UploadAsync(item.Image);

                    await productRepository.AddImageAsync(entity, image, cancellationToken);
                }

                await productRepository.SaveAsync(cancellationToken);
            }

            return entity;
        }
    }


    public class ImageItem
    {
        public int? Id { get; set; }
        public IFormFile Image { get; set; }
        public bool IsMain { get; set; }
    }
}
