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
        Task<Basket> AddBasketAsync(Product product, Basket basket, CancellationToken cancellation = default);
        IQueryable<Basket> GetBaskets(Expression<Func<Basket, bool>> predicate = null);
        EntityEntry<Basket> RemoveBasket(Basket basket);
    }
}
