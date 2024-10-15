using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Colors;
using WebUI.Services.Colors;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController(IColorService colorService) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var response = await colorService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await colorService.GetByIdAsync(id);
            return View(response.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Policy = "colors.add")]
        public async Task<IActionResult> Create(Color model)
        {
            await colorService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "colors.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await colorService.GetByIdAsync(id);
            return View(data.Data);
        }

        [HttpPost]
        [Authorize(Policy = "colors.edit")]
        public async Task<IActionResult> Edit(Color model)
        {
            await colorService.EditAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "colors.remove")]
        public async Task<IActionResult> Remove(int id)
        {
            await colorService.RemoveAsync(id);
            return NoContent();
        }
    }
}