using Domain.Entities;
using MediatR;

namespace Application.Modules.CategoriesModule.Commands.CategoryAddCommand
{
    public class CategoryAddRequest : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
