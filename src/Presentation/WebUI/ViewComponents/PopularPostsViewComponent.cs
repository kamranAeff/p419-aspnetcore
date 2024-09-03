using Microsoft.AspNetCore.Mvc;
using WebUI.Services.BlogPost;

namespace WebUI.ViewComponents
{
    public class PopularPostsViewComponent(IBlogPostService blogPostService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var response = await blogPostService.GetPopularsAsync(count);

            return View(response.Data);
        }
    }
}
