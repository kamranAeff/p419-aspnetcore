using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Common
{
    public abstract class AsyncRepository<T> : IAsyncRepository<T>
        where T : class
    {
        private readonly DbContext db;

        public AsyncRepository(DbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(T entry, CancellationToken cancellationToken = default)
        {
            await db.Set<T>().AddAsync(entry, cancellationToken);
        }

        public void Edit(T entry)
        {
            db.Entry(entry).State = EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            var query = db.Set<T>().AsQueryable();


            return query;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = db.Set<T>().AsQueryable();

            if (predicate is not null)
                query = query.Where(predicate);

            var entry = await query.FirstOrDefaultAsync(cancellationToken);

            if (entry is null)
                throw new ArgumentNullException();

            return entry!;
        }

        //        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        //        {
        //            var query = db.Set<T>().AsQueryable();

        //            var entry = await query.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        //#warning check entity is null or not
        //            return entry!;
        //        }

        public void Remove(T entry)
        {
            db.Set<T>().Remove(entry);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await db.SaveChangesAsync(cancellationToken);
        }
    }
}
