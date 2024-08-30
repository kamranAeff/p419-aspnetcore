using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Brands;
using WebUI.Services.Brands;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController(IBrandService brandService) : Controller
    {

        //[Authorize(Policy = "admin.brands.get")]
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var response = await brandService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.brands.get")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await brandService.GetByIdAsync(id);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.brands.add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Policy = "admin.brands.add")]
        public async Task<IActionResult> Create(Brand model)
        {
            await brandService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await brandService.GetByIdAsync(id);

            return View(data.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit(Brand model)
        {
            await brandService.EditAsync(model);

            return RedirectToAction("Index");
        }

        //[Authorize(Policy = "admin.brands.remove")]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await brandService.RemoveAsync(id);

            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
