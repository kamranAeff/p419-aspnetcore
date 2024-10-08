using MediatR;

namespace Application.Modules.SizesModule.Commands.SizeRemoveCommand
{
    public class SizeRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
