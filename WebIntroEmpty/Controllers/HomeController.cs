using Microsoft.AspNetCore.Mvc;

namespace WebIntroEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()   //home/index
        {
            return View();
        }

        public IActionResult About(int id,string name)   //home/about
        {
            return Content($"{id}) {name} | Haqqimizda {RandomId()}");
        }

        [NonAction]
        public string RandomId()   
        {
            return Guid.NewGuid().ToString();
        }
    }
}
