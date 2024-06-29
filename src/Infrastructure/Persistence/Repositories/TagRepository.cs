using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class TagRepository : AsyncRepository<Tag>, ITagRepository
    {
        public TagRepository(DbContext db) : base(db)
        {
        }
    }
}

