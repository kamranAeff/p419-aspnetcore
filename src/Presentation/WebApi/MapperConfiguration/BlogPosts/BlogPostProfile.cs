using AutoMapper;
using Domain.Entities;
using WebApi.MapperConfiguration.Converters;
using WebApi.Models;

namespace WebApi.MapperConfiguration.BlogPosts
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.Url, src => src.ConvertUsing(new FilePathConverter(), m => m.ImagePath))
                .ForMember(dest => dest.Content, src => src.MapFrom(m => m.Body))
                .ForMember(dest => dest.PublishDate, src => src.ConvertUsing(new DateTimeValueConverter(), m => m.PublishDate));
        }
    }

    

    
}
