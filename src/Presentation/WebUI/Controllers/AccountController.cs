using Domain.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<OganiUser> userManager;
        private readonly SignInManager<OganiUser> signInManager;

        public AccountController(UserManager<OganiUser> userManager, SignInManager<OganiUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

            await signInManager.SignInAsync(user, true);

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
