using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        
        [Route("/approve-account")]
        public async Task<IActionResult> RegisterComfirm(string token)
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }
    }
}
