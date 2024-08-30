using AutoMapper;
using WebUI.Models.DTOs.Blogs;

namespace WebUI.Mapper.BlogPostMapper
{
    class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogEditDto>()
                .ForMember(dest => dest.Image, src => src.Ignore())
                .ForMember(dest => dest.ImagePath, src => src.MapFrom(m => m.Image));
        }
    }
}
