using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class BrandRepository : AsyncRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext db) : base(db)
        {
        }
    }
}
