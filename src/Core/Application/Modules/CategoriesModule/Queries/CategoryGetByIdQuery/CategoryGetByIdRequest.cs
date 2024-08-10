using Domain.Entities;
using MediatR;

namespace Application.Modules.CategoriesModule.Queries.CategoryGetByIdQuery
{
    public class CategoryGetByIdRequest : IRequest<Category>
    {
        public int Id { get; set; }
    }
}
