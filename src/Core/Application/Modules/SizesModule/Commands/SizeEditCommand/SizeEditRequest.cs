using Domain.Entities;
using MediatR;

namespace Application.Modules.SizesModule.Commands.SizeEditCommand
{
    public class SizeEditRequest : IRequest<Size>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallName { get; set; }
    }
}
