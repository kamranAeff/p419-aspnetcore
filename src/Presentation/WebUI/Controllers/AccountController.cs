using Domain.Entities.Membership;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Common;
using System.Security.Claims;
using System.Web;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<OganiUser> userManager;
        private readonly SignInManager<OganiUser> signInManager;
        private readonly IEmailService emailService;

        public AccountController(UserManager<OganiUser> userManager, SignInManager<OganiUser> signInManager,IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new OganiUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View();
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            token = HttpUtility.UrlEncode(token);

            string link = $"{Request.Scheme}://{Request.Host}/approve-account?token={token}";


            string message = $"Salam hesabinizi tesdiq etmek ucun <a href=\"{link}\">link</a>`le davam edin";

            await emailService.SendEmail(email,"Approve Registration", message);

            return RedirectToAction("Index", "Home");
        }
        
        [Route("/approve-account")]
        public async Task<IActionResult> RegisterComfirm(string token)
        {
            var user = await userManager.FindByEmailAsync("akamran@code.edu.az");

            await userManager.ConfirmEmailAsync(user, token);

            return RedirectToAction("Index", "Home");
        }

        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [Route("/signin.html")]
        public async Task<IActionResult> Signin(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                ModelState.AddModelError("User", "username or password is incorrect");
                goto l1;
            }


            var result = await signInManager.CheckPasswordSignInAsync(user, password, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("User", "ypur account locked temporary.please try again after 5 minute!");
                goto l1;
            }
            else if (!result.Succeeded)
            {
                ModelState.AddModelError("User", "username or password is incorrect");
                goto l1;
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("User", "Email not comfirmend");
                goto l1;
            }

            //await signInManager.SignInAsync(user, true);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var now = DateTime.UtcNow;
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = now,
                ExpiresUtc = now.AddMinutes(2)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            var callbackUrl = Request.Query["ReturnUrl"];

            if (!string.IsNullOrWhiteSpace(callbackUrl))
            {
                return Redirect(callbackUrl);
            }

            return RedirectToAction("Index", "Home", routeValues: new { area = "" });
        l1:
            return View();
        }
    }
}
