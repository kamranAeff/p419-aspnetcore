using Application.Common;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.ProductPagesQuery
{
    class ProductPagedRequestHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository,
        IHttpContextAccessor ctx)
        : IRequestHandler<ProductPagedRequest, IPagedResponse<ProductsResponse>>
    {
        public async Task<IPagedResponse<ProductsResponse>> Handle(ProductPagedRequest request, CancellationToken cancellationToken)
        {
            var host = ctx.GetHost();

            var query = from p in productRepository.GetAll()
                        join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                        join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                        join b in brandRepository.GetAll() on p.BrandId equals b.Id
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

            return await query.Sort(request).ToPaginateAsync(request, cancellationToken);
        }
    }
}
