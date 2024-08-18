using Azure;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Blogs;
using WebUI.Services.BlogPost;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController(IBlogPostService service) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int size = 5)
        {
            var response = await service.GetPagedAsync(page, size);
            return View(response.Data);
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await service.GetByIdAsync(id);
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogAddDto model)
        {
            await service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await service.RemoveAsync(id);
            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
