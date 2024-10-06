using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Application.Modules.ProductsModule.Commands.BasketInteractCommand
{
    class BasketInteractRequestHandler(IProductRepository productRepository, IIdentityService identityService)
            : IRequestHandler<BasketInteractRequest, Basket>
    {
        public async Task<Basket> Handle(BasketInteractRequest request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;

            var entity = await productRepository.GetBaskets(m => m.ProductCardId == request.ProductCardId && m.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            if (entity is null && request.Count == 0)
                throw new NotFoundException($"{typeof(Basket).Name} not found by expression");

            var productCard = await productRepository.GetProductCards(m => m.Id == request.ProductCardId).FirstOrDefaultAsync(cancellationToken);

            if (productCard is null)
                throw new NotFoundException($"{typeof(ProductCard).Name} not found by expression");

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
            return entity;

        }
    }
}
