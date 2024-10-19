using Application.Extensions;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Commands.ProductAddCommand
{
    class ProductAddRequestHandler(IProductRepository productRepository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository,
        ISizeRepository sizeRepository,
        IColorRepository colorRepository,
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

            if (request.Cards is not null)
            {
                var cardColors = await colorRepository.GetAll(m => request.Cards.Select(m => m.ColorId).Contains(m.Id)).ToListAsync(cancellationToken);
                var cardSizes = await sizeRepository.GetAll(m => request.Cards.Select(m => m.SizeId).Contains(m.Id)).ToListAsync(cancellationToken);

                var cards = from card in request.Cards
                            join color in cardColors on card.ColorId equals color.Id
                            join size in cardSizes on card.SizeId equals size.Id
                            select new ProductCard
                            {
                                Id = Guid.NewGuid(),
                                ProductId = entity.Id,
                                SizeId = card.SizeId,
                                ColorId = card.ColorId,
                                Title = $"{entity.Title}, {color.Name}, {size.Name}",
                                Price = card.Price,
                                IsDefault = card.IsDefault
                            };

                foreach (var card in cards)
                {
                    card.Slug = card.Title.ToSlug();
                    await productRepository.AddCardAsync(entity, card, cancellationToken);
                }
                await productRepository.SaveAsync(cancellationToken);
            }

            return entity;
        }
    }
}
