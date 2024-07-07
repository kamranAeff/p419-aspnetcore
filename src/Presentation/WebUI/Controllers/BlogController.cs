using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BlogController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult LeaveComment(int id)
        {
            return View();
        }
    }
}
