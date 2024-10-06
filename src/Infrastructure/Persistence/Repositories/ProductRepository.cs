using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using Repositories.Common;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    class ProductRepository : AsyncRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db) { }

        public IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> predicate = null)
        {
            if (predicate is null)
                return this.db.Set<ProductImage>();

            return this.db.Set<ProductImage>().Where(predicate);
        }

        public async Task<ProductImage> AddImageAsync(Product product, ProductImage image, CancellationToken cancellation = default)
        {
            image.ProductId = product.Id;
            await db.Set<ProductImage>().AddAsync(image, cancellation);
            return image;
        }

        public EntityEntry<ProductImage> RemoveImage(ProductImage image) => db.Set<ProductImage>().Remove(image);

        public async Task<Basket> AddBasketAsync(Product product, Basket basket, CancellationToken cancellation = default)
        {
            basket.ProductId = product.Id;
            await db.Set<Basket>().AddAsync(basket, cancellation);
            return basket;
        }

        public IQueryable<Basket> GetBaskets(Expression<Func<Basket, bool>> predicate = null)
        {
            if (predicate is null)
                return this.db.Set<Basket>();

            return this.db.Set<Basket>().Where(predicate);
        }

        public EntityEntry<Basket> RemoveBasket(Basket basket) => db.Set<Basket>().Remove(basket);
    }
}
