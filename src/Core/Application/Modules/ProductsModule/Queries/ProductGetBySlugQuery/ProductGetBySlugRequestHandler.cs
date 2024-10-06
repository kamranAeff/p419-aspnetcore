using Application.Common;
using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.ProductGetBySlugQuery
{
    class ProductGetBySlugRequestHandler(IProductRepository productRepository,
      ICategoryRepository categoryRepository,
      IBrandRepository brandRepository,
      IColorRepository colorRepository,
      ISizeRepository sizeRepository,
      IHttpContextAccessor ctx) : IRequestHandler<ProductGetBySlugRequest, ProductsResponse>
    {
        public async Task<ProductsResponse> Handle(ProductGetBySlugRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = from p in productRepository.GetAll()
                        join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                        join pc in productRepository.GetCards(m => m.Slug.Equals(request.Slug)) on p.Id equals pc.ProductId
                        join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                        join b in brandRepository.GetAll() on p.BrandId equals b.Id
                        select new ProductsResponse
                        {
                            Id = p.Id,
                            Title = pc.Title,
                            Slug = pc.Slug,
                            Path = $"{host}/files/{pi.Path}",
                            BrandId = p.BrandId,
                            BrandName = b.Name,
                            CategoryId = p.CategoryId,
                            CategoryName = c.Name,
                            Rate = p.Rate,
                            Price = pc.Price,
                            Weight = p.Weight,
                            UnitOfWeight = p.UnitOfWeight,
                            Description = p.Description,
                            Information = p.Information,
                        };

            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException($"{typeof(Product).Name} not found by expression");

            entity.Images = await productRepository.GetImages(m => m.ProductId == entity.Id)
                                             .Select(m => new ImageItem
                                             {
                                                 Id = m.Id,
                                                 IsMain = m.IsMain,
                                                 TempPath = $"{host}/files/{m.Path}"
                                             }).ToListAsync(cancellationToken);

            entity.Cards = await (from card in productRepository.GetCards(m => m.ProductId == entity.Id)
                                  join color in colorRepository.GetAll() on card.ColorId equals color.Id
                                  join size in sizeRepository.GetAll() on card.SizeId equals size.Id
                                  select new ProductCardResponse
                                  {
                                      Id = card.Id,
                                      Slug = card.Slug,
                                      ColorId = color.Id,
                                      Color = color.Name,
                                      ColorHex = color.HexCode,
                                      SizeId = size.Id,
                                      Size = size.SmallName,
                                      IsDefault = card.Slug.Equals(request.Slug)
                                  }).ToListAsync(cancellationToken);

            return entity;
        }
    }
}