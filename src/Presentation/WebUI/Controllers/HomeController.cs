using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using Services;
using Services.Common;
using System.Text;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostEnvironment env;

        public HomeController(IHostEnvironment env)
        {
            this.env = env;
        }
        //private readonly IContactPostService contactPostService;
        //private readonly ISubscribeService subscribeService;

        //public HomeController(IContactPostService contactPostService,
        //    ISubscribeService subscribeService)
        //{
        //    this.contactPostService = contactPostService;
        //    this.subscribeService = subscribeService;
        //}
        public IActionResult Index()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            sb.AppendLine($"REDIS_HOST: {Environment.GetEnvironmentVariable("REDIS_HOST")}");
            sb.AppendLine($"REDIS_USER: {Environment.GetEnvironmentVariable("REDIS_USER")}");

            return Content(sb.ToString());

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Contact(string fullName, string email, string message)
        //{
        //    var resposeMessage = contactPostService.Add(fullName, email, message);

        //    return Json(new
        //    {
        //        error = false,
        //        message = resposeMessage
        //    });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Subscribe(string email)
        //{
        //    var response = await subscribeService.Subscribe(email);

        //    return Json(new
        //    {
        //        error = response.Item1,
        //        message = response.Item2
        //    });
        //}

        //[Route("/subscribe-approve")]
        //public async Task<IActionResult> SubscribeApprove(string token)
        //{
        //    var response = await subscribeService.SubscribeApprove(token);

        //    if (response.Item1)
        //    {
        //        ViewBag.ErrorMessage = response.Item2;
        //        return View();
        //    }

        //    TempData["Message"] = response.Item2;
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
