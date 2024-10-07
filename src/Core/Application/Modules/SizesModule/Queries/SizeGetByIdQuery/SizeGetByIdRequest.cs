using Domain.Entities;
using MediatR;

namespace Application.Modules.SizesModule.Queries.SizeGetByIdQuery
{
    public class SizeGetByIdRequest : IRequest<Size>
    {
        public int Id { get; set; }
    }
}
