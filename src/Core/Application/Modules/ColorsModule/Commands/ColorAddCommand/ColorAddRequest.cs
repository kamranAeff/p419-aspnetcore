using Domain.Entities;
using MediatR;

namespace Application.Modules.ColorsModule.Commands.ColorAddCommand
{
    public class ColorAddRequest : IRequest<Color>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
