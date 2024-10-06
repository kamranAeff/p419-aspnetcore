using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using Repositories.Common;
using System.Linq;
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

        public async Task<Basket> AddBasketAsync(ProductCard card, Basket basket, CancellationToken cancellation = default)
        {
            basket.ProductCardId = card.Id;
            basket.CreatedAt = DateTime.UtcNow.AddHours(4);
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

        public async Task<ProductCard> AddProductCardAsync(Product product, ProductCard card, CancellationToken cancellation = default)
        {
            card.ProductId = product.Id;
            await db.Set<ProductCard>().AddAsync(card, cancellation);
            return card;
        }

        public IQueryable<ProductCard> GetProductCards(Expression<Func<ProductCard, bool>> predicate = null)
        {
            if (predicate is null)
                return this.db.Set<ProductCard>();

            return this.db.Set<ProductCard>().Where(predicate);
        }

        public EntityEntry<ProductCard> RemoveBasket(ProductCard card) => db.Set<ProductCard>().Remove(card);
    }
}
