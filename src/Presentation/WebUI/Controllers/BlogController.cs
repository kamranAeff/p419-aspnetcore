using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Categories;
using WebUI.Services.BlogPost;

namespace WebUI.Controllers
{
    public class BlogController(IBlogPostService blogPostService) : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int size = 4)
        {
            var request = new CategoryGetPagedRequestDto
            {
                Column = "Id",
                Order = "ascending"
            };

            var response = await blogPostService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult LeaveComment(int id)
        {
            return View();
        }
    }
}
