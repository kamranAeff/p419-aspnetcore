using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.ProductGetByIdQuery
{
    class ProductGetByIdRequestHandler(IProductRepository productRepository,
     ICategoryRepository categoryRepository,
     IBrandRepository brandRepository,
     IHttpContextAccessor ctx) : IRequestHandler<ProductGetByIdRequest, ProductsResponse>
    {
        public async Task<ProductsResponse> Handle(ProductGetByIdRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = from p in productRepository.GetAll()
                        join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                        join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                        join b in brandRepository.GetAll() on p.BrandId equals b.Id
                        where p.Id == request.Id
                        select new ProductsResponse
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Slug = p.Slug,
                            Path = $"{host}/files/{pi.Path}",
                            BrandId = p.BrandId,
                            BrandName = b.Name,
                            CategoryId = p.CategoryId,
                            CategoryName = c.Name,
                            Rate = p.Rate,
                            Weight = p.Weight,
                            UnitOfWeight = p.UnitOfWeight,
                            Description = p.Description,
                            Information = p.Information,
                        };

            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException($"{typeof(Product).Name} not found by expression");

            return entity;
        }
    }
}
