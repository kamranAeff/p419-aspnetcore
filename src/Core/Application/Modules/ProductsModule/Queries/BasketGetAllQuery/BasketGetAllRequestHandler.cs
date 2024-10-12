using Application.Extensions;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Queries.BasketGetAllQuery
{
    class BasketGetAllRequestHandler(IProductRepository productRepository,
        IColorRepository colorRepository,
        ISizeRepository sizeRepository,
        IIdentityService identityService,
        IHttpContextAccessor ctx)
            : IRequestHandler<BasketGetAllRequest, BasketResponse>
    {
        public async Task<BasketResponse> Handle(BasketGetAllRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;
            var host = ctx.GetHost();

            var query = from p in productRepository.GetAll()
                        join pc in productRepository.GetCards() on p.Id equals pc.ProductId
                        join c in colorRepository.GetAll() on pc.ColorId equals c.Id
                        join s in sizeRepository.GetAll() on pc.SizeId equals s.Id
                        join b in productRepository.GetBaskets(m => m.UserId == userId) on pc.Id equals b.ProductCardId
                        join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                        select new BasketItem
                        {
                            Id = b.ProductCardId,
                            ProductId = p.Id,
                            Title = pc.Title,
                            Slug = pc.Slug,
                            Path = $"{host}/files/{pi.Path}",
                            Price = pc.Price,
                            Count = b.Count
                        };


            var response = new BasketResponse
            {
                Items = await query.Sort(request).ToListAsync(cancellationToken),
            };

            response.Total = response.Items.Sum(m => m.Subtotal);
            response.Coupon = "";
            response.DiscountedTotal = response.Total;

            return response;
        }
    }
}
