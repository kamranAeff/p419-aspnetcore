using MediatR;

namespace Application.Modules.ColorsModule.Commands.ColorRemoveCommand
{
    public class ColorRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
