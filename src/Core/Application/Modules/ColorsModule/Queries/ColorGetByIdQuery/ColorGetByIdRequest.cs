using Domain.Entities;
using MediatR;

namespace Application.Modules.ColorsModule.Queries.ColorGetByIdQuery
{
    public class ColorGetByIdRequest : IRequest<Color>
    {
        public int Id { get; set; }
    }
}
