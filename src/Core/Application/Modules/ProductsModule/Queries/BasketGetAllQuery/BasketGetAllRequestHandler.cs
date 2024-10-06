using Application.Extensions;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    class BasketGetAllRequestHandler(IProductRepository productRepository, IIdentityService identityService)
            : IRequestHandler<BasketGetAllRequest, IEnumerable<BasketItem>>
    {
        public async Task<IEnumerable<BasketItem>> Handle(BasketGetAllRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;

            var query = from p in productRepository.GetAll()
                        join b in productRepository.GetBaskets(m => m.UserId == userId) on p.Id equals b.ProductId
                        select new BasketItem
                        {
                            ProductId = p.Id,
                            ProductTitle = p.Title,
                            ProductSlug = p.Slug,
                            Count = b.Count
                        };

            return await query.Sort(request).ToListAsync(cancellationToken);
        }
    }
}
