using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class SizeRepository : AsyncRepository<Size>, ISizeRepository
    {
        public SizeRepository(DbContext db) : base(db)
        {
        }
    }
}
