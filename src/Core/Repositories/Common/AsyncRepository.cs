﻿using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Common
{
    public abstract class AsyncRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected readonly DbContext db;

        public AsyncRepository(DbContext db)
        {
            this.db = db;
        }

        public async virtual Task AddAsync(T entry, CancellationToken cancellationToken = default)
        {
            await db.Set<T>().AddAsync(entry, cancellationToken);
        }

        public virtual void Edit(T entry)
        {
            db.Entry(entry).State = EntityState.Modified;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var query = db.Set<T>().AsQueryable();

            if (predicate is not null)
                query = query.Where(predicate);

            return query;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = db.Set<T>().AsQueryable();

            if (predicate is not null)
                query = query.Where(predicate);

            var entry = await query.FirstOrDefaultAsync(cancellationToken);

            if (entry is null)
                throw new NotFoundException(typeof(T).Name);

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
