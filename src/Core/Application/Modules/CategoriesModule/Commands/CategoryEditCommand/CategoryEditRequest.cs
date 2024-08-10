using Domain.Entities;
using MediatR;

namespace Application.Modules.CategoriesModule.Commands.CategoryEditCommand
{
    public class CategoryEditRequest : IRequest<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
