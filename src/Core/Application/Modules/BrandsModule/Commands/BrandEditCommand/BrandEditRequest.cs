using Domain.Entities;
using MediatR;

namespace Application.Modules.BrandsModule.Commands.BrandEditCommand
{
    public class BrandEditRequest : IRequest<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
