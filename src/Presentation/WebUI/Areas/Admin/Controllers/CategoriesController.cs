using Microsoft.AspNetCore.Mvc;
using Services.Categories;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await categoryService.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await categoryService.GetByIdAsync(id);

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryRequestDto model)
        {
            await categoryService.AddAsync(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await categoryService.GetByIdAsync(id);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategortDto model)
        {
            await categoryService.EditAsync(model);

            return RedirectToAction("Index");
        }

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
