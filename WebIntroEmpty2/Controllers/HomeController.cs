using Microsoft.AspNetCore.Mvc;
using WebIntroEmpty2.AppCode.LoggingConcept;

namespace WebIntroEmpty2.Controllers
{
    public class HomeController(IMyLogger logger) : Controller
    {
        public IActionResult Index()
        {
            logger.WriteLog($"Home/Index");

            //ViewBag.GroupName = "Demo";
            ViewData["GroupName"] = 22;


            ViewBag.Fruits = new string[] { "Alma", "Heyva", "Nar" };
            ViewData["Groups"] = new int[] { 1, 2, 3 };


            TempData["Months"] = Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;

            //return View();
            return RedirectToAction(nameof(About));
            //return RedirectToAction("About","Students");
            //return View("About");
        }

        public IActionResult About()
        {
            logger.WriteLog($"Home/About");
            return View();
        }
    }
}
