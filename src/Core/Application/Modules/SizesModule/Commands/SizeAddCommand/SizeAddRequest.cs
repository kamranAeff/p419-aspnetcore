using Domain.Entities;
using MediatR;

namespace Application.Modules.SizesModule.Commands.SizeAddCommand
{
    public class SizeAddRequest : IRequest<Size>
    {
        public string Name { get; set; }
        public string SmallName { get; set; }
    }
}
