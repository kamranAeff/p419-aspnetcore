using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    class ProductRepository : AsyncRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

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
    }
}
