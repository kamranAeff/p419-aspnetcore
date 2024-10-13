using Microsoft.AspNetCore.Mvc;
using WebUI.Models.DTOs.Account;
using WebUI.Services.Account;

namespace WebUI.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [Route("/{lang:language}/approve-account")]
        public async Task<IActionResult> RegisterComfirm(string token)
        {
            return Content(token);
        }

        [Route("/{lang:language}/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [Route("/{lang:language}/signin.html")]
        public async Task<IActionResult> Signin(SignInRequestDto request)
        {
            var response = await accountService.SignInAsync(request);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/"
            };

            Response.Cookies.Append("accessToken", response.Data.AccessToken, options);
            Response.Cookies.Append("refreshToken", response.Data.RefreshToken, options);


            var callbackUrl = Request.Query["ReturnUrl"];

            if (!string.IsNullOrWhiteSpace(callbackUrl))
                return Redirect(callbackUrl);

            return RedirectToAction("Index", "Home", routeValues: new { area = "" });
        }
    }
}
