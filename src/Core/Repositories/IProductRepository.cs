using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Common;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> predicate = null);
        Task<ProductImage> AddImageAsync(Product product, ProductImage image, CancellationToken cancellation = default);
        EntityEntry<ProductImage> RemoveImage(ProductImage image);
        Task<Basket> AddBasketAsync(ProductCard card, Basket basket, CancellationToken cancellation = default);
        IQueryable<Basket> GetBaskets(Expression<Func<Basket, bool>> predicate = null);
        EntityEntry<Basket> RemoveBasket(Basket basket);
        Task<ProductCard> AddProductCardAsync(Product product, ProductCard card, CancellationToken cancellation = default);
        IQueryable<ProductCard> GetProductCards(Expression<Func<ProductCard, bool>> predicate = null);
        EntityEntry<ProductCard> RemoveBasket(ProductCard card);
    }
}
