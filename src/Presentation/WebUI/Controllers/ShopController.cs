using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ShopController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Basket()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
