using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.ContactPosts;
using WebUI.Services.ContactPosts;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController(IContactPostsService contactPostsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactPostApplyDto request)
        {
            var resposeMessage = await contactPostsService.ApplyAsync(request);

            return Json(resposeMessage);
        }

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