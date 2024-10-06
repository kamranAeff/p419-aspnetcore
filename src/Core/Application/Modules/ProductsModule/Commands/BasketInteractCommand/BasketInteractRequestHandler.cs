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

            var entity = await productRepository.GetBaskets(m => m.ProductId == request.ProductId && m.UserId == userId).FirstOrDefaultAsync(cancellationToken);

            if (entity is null && request.Count == 0)
                throw new NotFoundException($"{typeof(Basket).Name} not found by expression");

            var product = await productRepository.GetAsync(m => m.Id == request.ProductId, cancellationToken);

            if (entity is null)
            {
                entity = new Basket
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Count = request.Count
                };
                await productRepository.AddBasketAsync(product, entity, cancellationToken);
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
