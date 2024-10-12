using Application.Extensions;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repositories;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    class BasketInteractRequestHandler(IProductRepository productRepository,
        IColorRepository colorRepository,
        ISizeRepository sizeRepository,
        IIdentityService identityService,
        IHttpContextAccessor ctx)
            : IRequestHandler<BasketInteractRequest, BasketResponse>
    {
        public async Task<BasketResponse> Handle(BasketInteractRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;
            var host = ctx.GetHost();

            var entity = await productRepository.GetBaskets(m => m.ProductCardId == request.Id && m.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            if (entity is null && request.Count == 0)
                throw new NotFoundException(typeof(Basket).Name);

            var productCard = await productRepository.GetCards(m => m.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (productCard is null)
                throw new NotFoundException(typeof(ProductCard).Name);

            if (entity is null)
            {
                entity = new Basket
                {
                    UserId = userId,
                    Count = request.Count
                };
                await productRepository.AddBasketAsync(productCard, entity, cancellationToken);
            }
            else if (entity is not null && request.Count == 0)
                productRepository.RemoveBasket(entity);
            else
                entity.Count = request.Count;

            await productRepository.SaveAsync(cancellationToken);

            #region GetBasket

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
                Items = await query.ToListAsync(cancellationToken),
            };

            response.Total = response.Items.Sum(m => m.Subtotal);
            response.Coupon = "";
            response.DiscountedTotal = response.Total;
            #endregion

            return response;

        }
    }
}
