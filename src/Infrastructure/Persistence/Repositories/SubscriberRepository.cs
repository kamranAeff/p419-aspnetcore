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
    }
}
