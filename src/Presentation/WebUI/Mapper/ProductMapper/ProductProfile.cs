using AutoMapper;
using WebUI.Models.DTOs.Products;

namespace WebUI.Mapper.ProductMapper
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDetail, ProductEditDto>()
                .ForMember(dest => dest.Images, src => src.MapFrom(m => m.Images));
        }
    }
}