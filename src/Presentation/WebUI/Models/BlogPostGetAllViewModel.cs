using Domain.Entities;
using Services.Categories;

namespace WebUI.Models
{
    public class BlogPostGetAllViewModel
    {
        public IEnumerable<Subscribe> Subscribers { get; set; }
        public IEnumerable<CategoryGetAllDto> Categories { get; set; }
        public ContactPost ContactPost { get; set; }
        public string Token { get; set; }
    }
}
