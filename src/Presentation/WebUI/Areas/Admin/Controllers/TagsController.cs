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

        public async Task<IActionResult> Index()
        {
            var response = await tagService.GetAllASync();
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTagRequestDto model)
        {
            await tagService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
