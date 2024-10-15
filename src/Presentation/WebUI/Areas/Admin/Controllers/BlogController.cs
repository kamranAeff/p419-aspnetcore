using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Models.DTOs.Blogs;
using WebUI.Services.BlogPost;
using WebUI.Services.Categories;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController(IBlogPostService blogService, ICategoryService categoryService,IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int size = 5)
        {
            var response = await blogService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await blogService.GetByIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogAddDto model)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            await blogService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");

            var response = await blogService.GetByIdAsync(id);
            var data = mapper.Map<BlogEditDto>(response.Data);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] BlogEditDto model)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            await blogService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await blogService.RemoveAsync(id);
            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}