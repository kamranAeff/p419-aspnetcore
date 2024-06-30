using Domain.Entities;
using Repositories.Common;

namespace Repositories
{
    public interface IBlogPostRepository : IAsyncRepository<BlogPost>
    {
    }
}
