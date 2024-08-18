using Domain.Entities;
using Repositories.Common;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> predicate = null);
        Task<ProductImage> AddImageAsync(Product product, ProductImage image, CancellationToken cancellation = default);
        void RemoveImage(ProductImage image);
    }
}
