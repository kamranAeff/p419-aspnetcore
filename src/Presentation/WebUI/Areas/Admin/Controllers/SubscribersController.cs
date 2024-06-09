using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
