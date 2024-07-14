using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Tags;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly ITagService tagService;

        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [Authorize(Policy = "admin.tags.get")]
        public async Task<IActionResult> Index()
        {
            var response = await tagService.GetAllASync();
            return View(response);
        }

        [Authorize(Policy = "admin.tags.get")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await tagService.GetById(id);
            return View(response);
        }

        [Authorize(Policy = "admin.tags.add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "admin.tags.add")]
        public async Task<IActionResult> Create(AddTagRequestDto model)
        {
            await tagService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
