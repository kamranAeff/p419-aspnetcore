using Microsoft.AspNetCore.Mvc;
using WebUI.Proxies;
using WebUI.Proxies.BlogPostProxy;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogPostProxy proxy;

        public BlogController(IBlogPostProxy proxy)
        {
            this.proxy = proxy;
        }

        public async Task<IActionResult> Index()
        {
            var response = await proxy.GetAll();
            return View(response);
        }
    }
}
