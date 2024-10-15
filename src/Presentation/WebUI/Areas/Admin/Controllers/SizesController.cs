using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Sizes;
using WebUI.Services.Sizes;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizesController(ISizeService sizeService) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var response = await sizeService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await sizeService.GetByIdAsync(id);
            return View(response.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Policy = "sizes.add")]
        public async Task<IActionResult> Create(Size model)
        {
            await sizeService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "sizes.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await sizeService.GetByIdAsync(id);
            return View(data.Data);
        }

        [HttpPost]
        [Authorize(Policy = "sizes.edit")]
        public async Task<IActionResult> Edit(Size model)
        {
            await sizeService.EditAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "sizes.remove")]
        public async Task<IActionResult> Remove(int id)
        {
            await sizeService.RemoveAsync(id);
            return NoContent();
        }
    }
}