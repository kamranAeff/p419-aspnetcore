using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.ProductsGetAllQuery
{
    class ProductsGetAllRequestHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository,
        IHttpContextAccessor ctx)
        : IRequestHandler<ProductsGetAllRequest, IEnumerable<ProductsResponse>>
    {
        public async Task<IEnumerable<ProductsResponse>> Handle(ProductsGetAllRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = from p in productRepository.GetAll()
                        join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                        join pc in productRepository.GetCards(m => m.IsDefault == true) on p.Id equals pc.ProductId
                        join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                        join b in brandRepository.GetAll() on p.BrandId equals b.Id
                        select new ProductsResponse
                        {
                            Id = p.Id,
                            Title = p.Title,
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

            return await query.Sort(request).ToListAsync(cancellationToken);
        }
    }
}
