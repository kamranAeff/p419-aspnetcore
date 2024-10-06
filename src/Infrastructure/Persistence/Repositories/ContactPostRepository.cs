using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class ContactPostRepository : AsyncRepository<ContactPost>, IContactPostRepository
    {
        public ContactPostRepository(DbContext db) : base(db)
        {
        }

        public override Task AddAsync(ContactPost entry, CancellationToken cancellationToken = default)
        {
            entry.CreatedAt = DateTime.Now.AddHours(4);
            return base.AddAsync(entry, cancellationToken);
        }

        public override void Edit(ContactPost entry)
        {
            entry.AnsweredAt = DateTime.Now.AddHours(4);
            base.Edit(entry);
        }
    }
}
