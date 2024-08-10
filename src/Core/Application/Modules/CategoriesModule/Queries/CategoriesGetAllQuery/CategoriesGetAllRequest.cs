using Domain.Entities;
using MediatR;

namespace Application.Modules.CategoriesModule.Queries.CategoriesGetAllQuery
{
    public class CategoriesGetAllRequest : IRequest<IEnumerable<Category>>
    {
        
    }
}
