using Domain.Entities;
using Repositories.Common;

namespace Repositories
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
