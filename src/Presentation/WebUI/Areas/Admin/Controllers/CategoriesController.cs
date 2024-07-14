using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "admin.categories.get")]
        public async Task<IActionResult> Index()
        {
            var data = await categoryService.GetAllAsync();

            return View(data);
        }

        [Authorize(Policy = "admin.categories.get")]
        public async Task<IActionResult> Details(int id)
        {
            var data = await categoryService.GetByIdAsync(id);

            return View(data);
        }

        [Authorize(Policy = "admin.categories.add")]
        public IActionResult Create()
        {
            throw new ArgumentNullException();
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "admin.categories.add")]
        public async Task<IActionResult> Create(AddCategoryRequestDto model)
        {
            await categoryService.AddAsync(model);

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await categoryService.GetByIdAsync(id);

            return View(data);
        }

        [HttpPost]
        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(EditCategortDto model)
        {
            await categoryService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "admin.categories.remove")]
        public async Task<IActionResult> Remove(int id)
        {
            throw new ArgumentNullException();

            await categoryService.RemoveAsync(id);

            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
