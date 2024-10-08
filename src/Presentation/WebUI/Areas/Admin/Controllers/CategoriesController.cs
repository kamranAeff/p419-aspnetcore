using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Categories;
using WebUI.Services.Categories;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController(ICategoryService categoryService) : Controller
    {

        //[Authorize(Policy = "admin.categories.get")]
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var request = new CategoryGetPagedRequestDto
            {
                Column = "Id",
                Order = "ascending"
            };

            var response = await categoryService.GetPagedAsync(page, size, request);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.categories.get")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await categoryService.GetByIdAsync(id);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.categories.add")]
        public IActionResult Create() => View();

        [HttpPost]
        //[Authorize(Policy = "admin.categories.add")]
        public async Task<IActionResult> Create(Category model)
        {
            await categoryService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await categoryService.GetByIdAsync(id);

            return View(data.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(Category model)
        {
            await categoryService.EditAsync(model);

            return RedirectToAction("Index");
        }

        //[Authorize(Policy = "admin.categories.remove")]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await categoryService.RemoveAsync(id);

            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
