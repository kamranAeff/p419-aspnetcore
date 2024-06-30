using Microsoft.AspNetCore.Mvc;
using Services.BlogPosts;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogPostService blogPostService;

        public BlogController(IBlogPostService blogPostService)
        {
            this.blogPostService = blogPostService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await blogPostService.GetAll();
            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await blogPostService.GetById(id);
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddBlogPostRequestDto request)
        {
            Console.WriteLine("$$###############################demo");
            await blogPostService.AddAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
