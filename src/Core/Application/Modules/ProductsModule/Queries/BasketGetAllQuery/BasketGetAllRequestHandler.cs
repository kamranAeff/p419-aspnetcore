using Application.Extensions;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    class BasketGetAllRequestHandler(IProductRepository productRepository,
        IColorRepository colorRepository,
        ISizeRepository sizeRepository,
        IIdentityService identityService)
            : IRequestHandler<BasketGetAllRequest, BasketResponse>
    {
        public async Task<BasketResponse> Handle(BasketGetAllRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;

            var query = from p in productRepository.GetAll()
                        join pc in productRepository.GetProductCards() on p.Id  equals pc.ProductId
                        join c in colorRepository.GetAll() on pc.ColorId  equals c.Id
                        join s in sizeRepository.GetAll() on pc.SizeId  equals s.Id
                        join b in productRepository.GetBaskets(m => m.UserId == userId) on pc.Id equals b.ProductCardId
                        select new BasketItem
                        {
                            ProductId = p.Id,
                            ProductTitle = $"{p.Title} {c.Name} {s.SmallName}",
                            ProductSlug = p.Slug,
                            Price = pc.Price,
                            Count = b.Count
                        };


            var response = new BasketResponse
            {
                List = await query.Sort(request).ToListAsync(cancellationToken),
            };

            response.Total = response.List.Sum(m=>m.Subtotal);
            response.Coupon = "";
            response.DiscountedTotal = response.Total;

            return response;
        }
    }
}
