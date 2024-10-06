using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class SubscriberRepository : AsyncRepository<Subscribe>, ISubscriberRepository
    {
        public SubscriberRepository(DbContext db) : base(db)
        {
        }

        public override Task AddAsync(Subscribe entry, CancellationToken cancellationToken = default)
        {
            entry.CreatedAt = DateTime.UtcNow.AddHours(4);
            return base.AddAsync(entry, cancellationToken);
        }
    }
}
