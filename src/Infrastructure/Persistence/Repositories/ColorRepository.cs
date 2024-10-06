using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;

namespace Persistence.Repositories
{
    class ColorRepository : AsyncRepository<Color>, IColorRepository
    {
        public ColorRepository(DbContext db) : base(db)
        {
        }
    }
}
