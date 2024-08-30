using Domain.Entities;
using MediatR;

namespace Application.Modules.BrandsModule.Commands.BrandAddCommand
{
    public class BrandAddRequest : IRequest<Brand>
    {
        public string Name { get; set; }
    }
}
