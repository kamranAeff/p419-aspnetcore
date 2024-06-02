using Microsoft.AspNetCore.Mvc;

namespace Ogani.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
