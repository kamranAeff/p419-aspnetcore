using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Tags;
using WebUI.Services.Tags;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController(ITagService tagService) : Controller
    {

        //[Authorize(Policy = "admin.tags.get")]
        public async Task<IActionResult> Index(int page = 1, int size = 15)
        {
            var response = await tagService.GetPagedAsync(page, size);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.tags.get")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await tagService.GetByIdAsync(id);
            return View(response.Data);
        }

        //[Authorize(Policy = "admin.tags.add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Policy = "admin.tags.add")]
        public async Task<IActionResult> Create(Tag model)
        {
            await tagService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Policy = "admin.tags.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await tagService.GetByIdAsync(id);

            return View(data.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "admin.tags.edit")]
        public async Task<IActionResult> Edit(Tag model)
        {
            await tagService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }

        //[Authorize(Policy = "admin.tags.remove")]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await tagService.RemoveAsync(id);

            return Json(new
            {
                error = false,
                message = "ok"
            });
        }
    }
}
