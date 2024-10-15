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
        public async Task<IActionResult> Details(string slug)
        {
            var response = await blogPostService.GetBySlugAsync(slug);
            return View(response.Data);
        }

        public IActionResult LeaveComment(int id)
        {
            return View();
        }
    }
}