using Domain.Entities;
using MediatR;

namespace Application.Modules.BrandsModule.Queries.BrandGetByIdQuery
{
    public class BrandGetByIdRequest : IRequest<Brand>
    {
        public int Id { get; set; }
    }
}
